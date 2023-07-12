using HypeFire.Library.Patterns.Observer;
using UnityEngine.SceneManagement;
using LeveGenerator.Data;
using UnityEngine;
using UI;

namespace Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager GloballAccess { get; private set; }

		private float _startDelay = .1f;

		public bool isAILevel;
		public bool isLevelStarted;
		public float levelLifeTime;
		public LevelData levelData;
		[field: SerializeField] public int objectCount { get; set; } = 0;

		public PublisherContainer<ObserverData<bool>> completedInfoPublisher =
			new PublisherContainer<ObserverData<bool>>();

		public PublisherContainer<ObserverData<bool>> startedInfoPublisher =
			new PublisherContainer<ObserverData<bool>>();


		private void Awake()
		{
			GloballAccess = this;
			if (objectCount < levelData.objectCount)
			{
				objectCount = levelData.objectCount;
			}
		}

		public void Update()
		{
			if (!isLevelStarted)
			{
				if (_startDelay > 0f)
				{
					_startDelay -= Time.deltaTime;
					return;
				}


				isLevelStarted = true;
				startedInfoPublisher.OnNotify(
					new ObserverData<bool>(this.gameObject, isLevelStarted));
			}
		}

		public void LevelCompleted(int score)
		{
			isLevelStarted = false;

			if (isAILevel)
			{
				if (Player.PlayerController.GloballAccess.collectorHandle.count < (objectCount * .5))
				{
					LevelFailed(0);
					return;
				}
				else
				{
					score = Player.PlayerController.GloballAccess.collectorHandle.count;
				}
			}

			startedInfoPublisher.OnNotify(new ObserverData<bool>(gameObject, false));
			completedInfoPublisher.OnNotify(new ObserverData<bool>(gameObject, true));
			UIController.GloballAccess.OnSuccessMenu(score);
		}

		public void LevelFailed(int score)
		{
			isLevelStarted = true;
			startedInfoPublisher.OnNotify(new ObserverData<bool>(gameObject, false));
			completedInfoPublisher.OnNotify(new ObserverData<bool>(gameObject, true));
			UIController.GloballAccess.OnFailedMenu();
		}

		public void NextLevel()
		{
			var levelId = SceneManager.GetActiveScene().buildIndex;
			if (levelId < SceneManager.sceneCountInBuildSettings - 1)
			{
				SceneManager.LoadScene(levelId + 1);
			}
			else
			{
				SceneManager.LoadScene(0);
			}
		}

		public void Retry()
		{
			var levelId = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(levelId);
		}
	}
}