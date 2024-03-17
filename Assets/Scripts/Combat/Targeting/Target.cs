using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestoryed;

    private void OnDestroy()
    {
        // 파괴됐을때 Action 이벤트 호출
        OnDestoryed?.Invoke(this);
    }
}
