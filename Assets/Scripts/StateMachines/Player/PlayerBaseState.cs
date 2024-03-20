using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine m_stateMachine;

    public PlayerBaseState(PlayerStateMachine argStateMahcine)
    {
        this.m_stateMachine = argStateMahcine;
    }

    protected void Move(Vector3 argMovement, float argDeltaTime)
    {
        m_stateMachine.Controller.Move((argMovement + m_stateMachine.ForeceReceiver.Movement) * argDeltaTime);
    }
}
