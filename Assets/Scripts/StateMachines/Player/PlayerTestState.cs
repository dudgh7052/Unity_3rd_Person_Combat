using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float m_timer;

    public PlayerTestState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.m_jumpEvent += OnJump;
    }

    public override void Tick(float argDeltaTime)
    {
        m_timer += argDeltaTime;

        Debug.Log(m_timer);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.m_jumpEvent -= OnJump;
    }

    void OnJump()
    {
        m_stateMachine.SwitchState(new PlayerTestState(m_stateMachine));
    }
}