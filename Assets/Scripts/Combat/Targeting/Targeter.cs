using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    /// <summary>
    /// Å¸°Ù ¸®½ºÆ®
    /// </summary>
    private List<Target> m_targets = new List<Target>();

    /// <summary>
    /// ÇöÀç Å¸°Ù
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
    /// Å¸°Ù °í¸£±â
    /// </summary>
    public bool SelectTarget()
    {
        if (m_targets.Count == 0) return false;

        CurrentTarget = m_targets[0];

        return true;
    }

    /// <summary>
    /// Å¸°Ù Äµ½½
    /// </summary>
    public void Cancel()
    {
        CurrentTarget = null;
    }
}
