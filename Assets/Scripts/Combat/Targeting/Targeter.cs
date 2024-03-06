using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    /// <summary>
    /// Å¸°Ù
    /// </summary>
    public List<Target> m_targets = new List<Target>();

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
}
