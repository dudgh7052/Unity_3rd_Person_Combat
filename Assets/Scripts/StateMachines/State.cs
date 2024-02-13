using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(); // 상태 처음 호출
    public abstract void Tick(float argDeltaTime); // 계속 호출
    public abstract void Exit(); // 상태 나갈때 호출
}
