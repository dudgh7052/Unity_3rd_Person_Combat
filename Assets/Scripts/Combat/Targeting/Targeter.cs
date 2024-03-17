using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    /// <summary>
    /// �ó׸ӽ� Ÿ�� �׷�
    /// </summary>
    [SerializeField] private CinemachineTargetGroup m_cineTargetGroup;

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

        // ���� Ÿ���� Action �̺�Ʈ�� RemoveTarget() �Ҵ�
        _target.OnDestoryed += RemoveTarget; 
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Target _target)) return;

        RemoveTarget(_target);
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    public bool SelectTarget()
    {
        if (m_targets.Count == 0) return false;

        CurrentTarget = m_targets[0];

        // Ÿ�� �׷��� ��� �߰�
        m_cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    /// <summary>
    /// Ÿ�� ĵ��
    /// </summary>
    public void Cancel()
    {
        if (CurrentTarget == null) return;

        // Ÿ�� �׷� ��� ����
        m_cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    /// <summary>
    /// Ÿ�ٱ׷쿡�� ����
    /// </summary>
    /// <param name="argTarget">��ǥ</param>
    void RemoveTarget(Target argTarget)
    {
        if (CurrentTarget == argTarget)
        {
            m_cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        argTarget.OnDestoryed -= RemoveTarget; // Ÿ�ٿ��� Action �Ҵ� ����
        m_targets.Remove(argTarget); // ����Ʈ���� ����
    }
}
