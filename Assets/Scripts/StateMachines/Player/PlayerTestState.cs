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
        Vector3 _movement = CalculateMovement();
       
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

    private Vector3 CalculateMovement()
    {
        Vector3 _cameraForward = m_stateMachine.MainCameraTransform.forward;
        Vector3 _cameraRight = m_stateMachine.MainCameraTransform.right;

        _cameraForward.y = 0f;
        _cameraRight.y = 0f;

        _cameraForward.Normalize();
        _cameraRight.Normalize();

        // ī�޶� Forward�� 1�̰� * �Ʒ� ����Ű �Է��� -1�̸� 1 * -1 = -1 �̹Ƿ� �ڷ� �̵��ϰ� ��, ���̵嵵 �Ȱ��� ����
        return _cameraForward * m_stateMachine.InputReader.MovementValue.y +
            _cameraRight * m_stateMachine.InputReader.MovementValue.x;
    }
}