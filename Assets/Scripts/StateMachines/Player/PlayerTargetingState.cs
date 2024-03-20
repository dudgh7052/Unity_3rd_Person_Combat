using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    public PlayerTargetingState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.CancelEvent += OnCancel;

        m_stateMachine.Animator.Play(TargetingBlendTreeHash); // BlendTree 바꿔주기
    }

    public override void Tick(float argDeltaTime)
    {
        if (m_stateMachine.Targeter.CurrentTarget == null)
        {
            m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
        }

        Vector3 _movement = CalculateMovement();

        Move(_movement * m_stateMachine.TargetingMovementSpeed, argDeltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel() 
    {
        m_stateMachine.Targeter.Cancel();

        m_stateMachine.SwitchState(new PlayerFreeLookState(m_stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 _movement = new Vector3();

        // 현재 transform의 right와 forward에 각각 방향 입력을 곱해주기
        _movement += m_stateMachine.transform.right * m_stateMachine.InputReader.MovementValue.x;
        _movement += m_stateMachine.transform.forward * m_stateMachine.InputReader.MovementValue.y;

        return _movement;
    }
}
