using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    /// <summary>
    /// Ÿ�� ����Ʈ
    /// </summary>
    private List<Target> m_targets = new List<Target>();

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Target _target)) return;

        m_targets.Add(_target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Target _target)) return;

        m_targets.Remove(_target);
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    public bool SelectTarget()
    {
        if (m_targets.Count == 0) return false;

        CurrentTarget = m_targets[0];

        return true;
    }

    /// <summary>
    /// Ÿ�� ĵ��
    /// </summary>
    public void Cancel()
    {
        CurrentTarget = null;
    }
}
