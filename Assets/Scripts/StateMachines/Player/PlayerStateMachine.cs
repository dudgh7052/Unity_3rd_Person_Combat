using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; } // �Է� ���� ��ũ��Ʈ
    [field: SerializeField] public CharacterController Controller { get; private set; } // ĳ���� ��Ʈ�ѷ�

    [field: SerializeField] public ForceReceiver ForeceReceiver { get; private set; } // �߷� ����

    [field: SerializeField] public Animator Animator { get; private set; } // �ִϸ�����
    [field: SerializeField] public Targeter Targeter { get; private set; } // Ÿ����
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; } // �⺻ �̵��ӵ�

    [field: SerializeField] public float TargetingMovementSpeed { get; private set; } // Ÿ�� ������Ʈ �̵��ӵ�
    [field: SerializeField] public float RotationDamping { get; private set; } // �⺻ �̵��ӵ�

    public Transform MainCameraTransform { get; private set; } // �ִϸ�����

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
