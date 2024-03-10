using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.CancelEvent += OnCancel;
    }

    public override void Tick(float argDeltaTime)
    {
        Debug.Log(m_stateMachine.Targeter.CurrentTarget.name);
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
}
