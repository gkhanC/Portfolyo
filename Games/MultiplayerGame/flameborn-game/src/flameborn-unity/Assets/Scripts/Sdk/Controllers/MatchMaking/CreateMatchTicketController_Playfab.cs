using System;
using flameborn.Core.User;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.MultiplayerModels;

namespace flameborn.Sdk.Controllers.MatchMaking
{
    /// <summary>
    /// Controller for creating a match ticket via PlayFab.
    /// </summary>
    [Serializable]
    public class CreateMatchTicketController_Playfab : Controller<ICreateMatchTicketResponse>, IApiController<ICreateMatchTicketResponse>
    {
        #region Fields

        private string queueName;
        private UserData userData;
        private event Action<ICreateMatchTicketResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMatchTicketController_Playfab"/> class.
        /// </summary>
        /// <param name="queue">The name of the queue.</param>
        /// <param name="user">The user data for the player creating the ticket.</param>
        public CreateMatchTicketController_Playfab(string queue, UserData user)
        {
            queueName = queue;
            userData = user;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to create a match ticket.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<ICreateMatchTicketResponse>[] listeners)
        {
            errorLog = "";

            if (string.IsNullOrEmpty(queueName)) 
            { 
                errorLog = $"{nameof(queueName)} is null or empty."; 
            }
            if (userData.IsNull()) 
            { 
                errorLog = $"{nameof(userData)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.CreateMatchmakingTicket(request, OnCreatedMatchTicket_EventListener_Playfab, OnError);
        }

        /// <summary>
        /// Takes the request for creating a match ticket.
        /// </summary>
        /// <returns>The request to create a match ticket.</returns>
        private CreateMatchmakingTicketRequest TakeRequest()
        {
            return new CreateMatchmakingTicketRequest
            {
                QueueName = queueName,
                GiveUpAfterSeconds = 30,
                Creator = new MatchmakingPlayer
                {
                    Entity = new EntityKey
                    {
                        Id = PlayFabSettings.staticPlayer.EntityId,
                        Type = PlayFabSettings.staticPlayer.EntityType
                    },
                    Attributes = new MatchmakingPlayerAttributes
                    {
                        DataObject = new PlayerData
                        (
                            PlayFabSettings.staticPlayer.PlayFabId,
                            userData.UserName,
                            userData.Rating.ToString(),
                            userData.Rank.ToString()
                        )
                    }
                }
            };
        }

        /// <summary>
        /// Handles the event when the match ticket creation result is received.
        /// </summary>
        /// <param name="result">The result of the match ticket creation request.</param>
        private void OnCreatedMatchTicket_EventListener_Playfab(CreateMatchmakingTicketResult result)
        {
            var response = new CreateMatchTicketResponse();
            response.IsRequestSuccess = !string.IsNullOrEmpty(result.TicketId);

            if (response.IsRequestSuccess)
            {
                response.SetResponse(true, result.TicketId, result, "New ticket saved.");
            }
            else
            {
                response.Message = "Match ticket creation failed.";
            }

            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the match ticket creation request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new CreateMatchTicketResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
