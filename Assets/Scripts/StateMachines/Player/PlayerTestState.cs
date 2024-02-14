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

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            // SetFloat(Parameter 이름, 설정값, 점점 줄어들 값, 시간)
            // 매직넘버 쓰지말고 나중에는 변수로 선언해서 넣기, 파라미터명도 StringHash 사용해서 사용
            m_stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, argDeltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, argDeltaTime);
        m_stateMachine.transform.rotation = Quaternion.LookRotation(_movement);
    }

    public override void Exit()
    {

    }
}