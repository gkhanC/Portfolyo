using Managers;
using UnityEngine;


public class AnimationListener : MonoBehaviour
{
    public void AnimationCallBack(string callerParameter)
    {
        switch (callerParameter)
        {
            case "shot":
                GameManager.GetInstance().player.OnShot();
                break;
            default:
                break;
        }
    }

    public void AnimationCallBackCompleted(string callerParameter)
    {
        switch (callerParameter)
        {
            case "shotCompleted":
                GameManager.GetInstance().player.ShotCompleted();
                GameManager.GetInstance().player.characterAnimator
                    .SetBodyState(CharacterAnimator.BodyAnimationState.IdleNormal);
                break;
            case "damage":
                GameManager.GetInstance().player.characterAnimator.PlayDamageAnimation(false);
                break;
            default:
                break;
        }
    }
}