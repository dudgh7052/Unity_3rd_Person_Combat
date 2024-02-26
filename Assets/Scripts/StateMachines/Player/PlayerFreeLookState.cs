using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 _movement = CalculateMovement();
       
        m_stateMachine.Controller.Move(_movement * m_stateMachine.FreeLookMovementSpeed * deltaTime); // �̵�

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            // SetFloat(Parameter �̸�, ������, ���� �پ�� ��, �ð�)
            m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(_movement, deltaTime);
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// ī�޶� �̵� ���
    /// </summary>
    /// <returns>�̵� ����</returns>
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

    /// <summary>
    /// �÷��̾� ȸ��
    /// </summary>
    /// <param name="argMovement">�̵� ����</param>
    private void FaceMovementDirection(Vector3 argMovement, float deltaTime)
    {
        m_stateMachine.transform.rotation = Quaternion.Lerp(
            m_stateMachine.transform.rotation,
            Quaternion.LookRotation(argMovement),
            deltaTime * m_stateMachine.RotationDamping);
    }
}