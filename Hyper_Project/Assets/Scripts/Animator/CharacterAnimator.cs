using System;
using System.Collections;
using System.Collections.Generic;
using Guns.Abstract;
using HypeFire.Library.Utilities.Extensions.Object;
using Managers;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [field: SerializeField] private Animator _animator;

    [field: SerializeField] private BodyAnimationState _body_animation_state;
    [field: SerializeField] private FootAnimationState _foot_animation_state;

    private static readonly int runDirection = Animator.StringToHash("forward");
    private static readonly int runSpeed = Animator.StringToHash("speed");
    private static readonly int shot = Animator.StringToHash("shot");
    private static readonly int die = Animator.StringToHash("die");
    private static readonly int damage = Animator.StringToHash("damage");


    private void Start()
    {
        if (_animator.IsNull())
            _animator = GetComponent<Animator>();
    }

    public void SetGun(IGun gun)
    {
        _animator.runtimeAnimatorController = gun.animatorController;
    }

    public void PlayShotAnimation()
    {
        _animator.SetTrigger(shot);
        GameManager.GetInstance().player.ShotInvoke();
        SetBodyState(BodyAnimationState.Shot);
    }

    public void PlayDieAnimation()
    {
        _animator.SetBool(die, true);
        SetBodyState(BodyAnimationState.Die);
    }

    public void PlayDamageAnimation(bool directive)
    {
        _animator.SetBool(damage, directive);
        GameManager.GetInstance().player.ShotCompleted();
    }

    public void SetRunAnimationParameter(float direction)
    {
        _animator.SetFloat(runDirection, direction);
        _foot_animation_state = direction < .1f ? FootAnimationState.Idle :
            direction <= 1f ? FootAnimationState.RunForward : FootAnimationState.RunBackward;
    }

    public void SetBodyState(BodyAnimationState state)
    {
        _body_animation_state = state;
    }

    public void SetRunSpeedAnimationParameter(float speed)
    {
        _animator.SetFloat(runSpeed, speed);
    }

    public enum BodyAnimationState
    {
        IdleNormal = 0,
        IdleBattle = 1,
        Hit = 2,
        Shot = 3,
        Die = 4
    }

    public enum FootAnimationState
    {
        Idle = 0,
        RunForward = 1,
        RunBackward = 2
    }
}