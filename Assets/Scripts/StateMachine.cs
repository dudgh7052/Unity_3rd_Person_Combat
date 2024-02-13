using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State m_currentState;

    void Update()
    {
        // m_currentState�� null���� ?�����ڷ� üũ�ϰ� �ƴϸ� Tick() ȣ��
        m_currentState?.Tick(Time.deltaTime);
    }

    /// <summary>
    /// State �ٲٱ�
    /// </summary>
    /// <param name="argState">�ٲ� State</param>
    public void SwitchState(State argState)
    {
        m_currentState?.Exit();
        m_currentState = argState;
        m_currentState?.Enter();
    }
}
