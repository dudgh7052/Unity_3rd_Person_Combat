using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(); // ���� ó�� ȣ��
    public abstract void Tick(float argDeltaTime); // ��� ȣ��
    public abstract void Exit(); // ���� ������ ȣ��
}
