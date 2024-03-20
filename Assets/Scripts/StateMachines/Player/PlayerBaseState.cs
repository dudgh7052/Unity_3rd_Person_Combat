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

    protected void FaceTarget()
    {
        if (m_stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 _lookPos = m_stateMachine.Targeter.CurrentTarget.transform.position - m_stateMachine.transform.position;
        _lookPos.y = 0.0f;

        m_stateMachine.transform.rotation = Quaternion.LookRotation(_lookPos); // 바라보기
    }
}
