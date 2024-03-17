using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestoryed;

    private void OnDestroy()
    {
        // �ı������� Action �̺�Ʈ ȣ��
        OnDestoryed?.Invoke(this);
    }
}
