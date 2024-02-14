using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; } // 입력 관리 스크립트
    [field: SerializeField] public CharacterController Controller { get; private set; } // 캐릭터 컨트롤러
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
