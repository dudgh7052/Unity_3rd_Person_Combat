using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine argStateMahcine) : base(argStateMahcine) { }

    public override void Enter()
    {
        m_stateMachine.InputReader.TargetEvent += OnTarget;

        m_stateMachine.Animator.Play(FreeLookBlendTreeHash); // BlendTree 바꿔주기
    }

    public override void Tick(float deltaTime)
    {
        if (m_stateMachine.InputReader.IsAttacking)
        {
            m_stateMachine.SwitchState(new PlayerAttackingState(m_stateMachine));
            return;
        }

        Vector3 _movement = CalculateMovement();

        Move(_movement * m_stateMachine.FreeLookMovementSpeed, deltaTime); // 이동 

        if (m_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            // SetFloat(Parameter 이름, 설정값, 점점 줄어들 값, 시간)
            m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        m_stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(_movement, deltaTime);
    }

    public override void Exit()
    {
        m_stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (!m_stateMachine.Targeter.SelectTarget()) return;

        m_stateMachine.SwitchState(new PlayerTargetingState(m_stateMachine));
    }

    /// <summary>
    /// 카메라 이동 계산
    /// </summary>
    /// <returns>이동 방향</returns>
    private Vector3 CalculateMovement()
    {
        Vector3 _cameraForward = m_stateMachine.MainCameraTransform.forward;
        Vector3 _cameraRight = m_stateMachine.MainCameraTransform.right;

        _cameraForward.y = 0f;
        _cameraRight.y = 0f;

        _cameraForward.Normalize();
        _cameraRight.Normalize();

        // 카메라 Forward이 1이고 * 아래 방향키 입력이 -1이면 1 * -1 = -1 이므로 뒤로 이동하게 됨, 사이드도 똑같이 적용
        return _cameraForward * m_stateMachine.InputReader.MovementValue.y +
            _cameraRight * m_stateMachine.InputReader.MovementValue.x;
    }

    /// <summary>
    /// 플레이어 회전
    /// </summary>
    /// <param name="argMovement">이동 방향</param>
    private void FaceMovementDirection(Vector3 argMovement, float deltaTime)
    {
        m_stateMachine.transform.rotation = Quaternion.Lerp(
            m_stateMachine.transform.rotation,
            Quaternion.LookRotation(argMovement),
            deltaTime * m_stateMachine.RotationDamping);
    }
}