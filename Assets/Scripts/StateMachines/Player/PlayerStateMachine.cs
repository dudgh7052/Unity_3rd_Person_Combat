using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; } // �Է� ���� ��ũ��Ʈ
    [field: SerializeField] public CharacterController Controller { get; private set; } // ĳ���� ��Ʈ�ѷ�
    [field: SerializeField] public Animator Animator { get; private set; } // �ִϸ�����
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; } // �⺻ �̵��ӵ�

    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
}
