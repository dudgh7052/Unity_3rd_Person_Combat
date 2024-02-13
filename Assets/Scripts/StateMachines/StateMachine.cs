using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State m_currentState;

    void Update()
    {
        // m_currentState가 null인지 ?연산자로 체크하고 아니면 Tick() 호출
        m_currentState?.Tick(Time.deltaTime);
    }

    /// <summary>
    /// State 바꾸기
    /// </summary>
    /// <param name="argState">바꿀 State</param>
    public void SwitchState(State argState)
    {
        m_currentState?.Exit();
        m_currentState = argState;
        m_currentState?.Enter();
    }
}
