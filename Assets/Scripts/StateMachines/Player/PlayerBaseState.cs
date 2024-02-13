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
}
