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
        _movement.x = m_stateMachine.InputReader.MovementValue.x; // ������, ����
        _movement.y = 0;
        _movement.z = m_stateMachine.InputReader.MovementValue.y; // ��, ��

        m_stateMachine.Controller.Move(_movement * m_stateMachine.FreeLookMovementSpeed * Time.deltaTime); // �̵�

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            // SetFloat(Parameter �̸�, ������, ���� �پ�� ��, �ð�)
            // �����ѹ� �������� ���߿��� ������ �����ؼ� �ֱ�, �Ķ���͸� StringHash ����ؼ� ���
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