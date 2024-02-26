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
}