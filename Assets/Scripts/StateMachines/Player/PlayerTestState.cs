using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {

    }

    public override void Tick(float argDeltaTime)
    {
        Vector3 _movement = new Vector3();
        _movement.x = m_stateMachine.InputReader.MovementValue.x; // 오른쪽, 왼쪽
        _movement.y = 0;
        _movement.z = m_stateMachine.InputReader.MovementValue.y; // 앞, 뒤

        m_stateMachine.Controller.Move(_movement * m_stateMachine.FreeLookMovementSpeed * Time.deltaTime); // 이동

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero) return;

        m_stateMachine.transform.rotation = Quaternion.LookRotation(_movement);
    }

    public override void Exit()
    {

    }
}