using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; } // 입력 관리 스크립트
    [field: SerializeField] public CharacterController Controller { get; private set; } // 캐릭터 컨트롤러
    [field: SerializeField] public Animator Animator { get; private set; } // 애니메이터
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; } // 기본 이동속도
    [field: SerializeField] public float RotationDamping { get; private set; } // 기본 이동속도

    public Transform MainCameraTransform { get; private set; } // 애니메이터

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
