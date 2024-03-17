using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    /// <summary>
    /// 시네머신 타겟 그룹
    /// </summary>
    [SerializeField] private CinemachineTargetGroup m_cineTargetGroup;

    /// <summary>
    /// 타겟 리스트
    /// </summary>
    private List<Target> m_targets = new List<Target>();

    /// <summary>
    /// 현재 타겟
    /// </summary>
    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Target _target)) return;

        m_targets.Add(_target);

        // 들어온 타겟의 Action 이벤트에 RemoveTarget() 할당
        _target.OnDestoryed += RemoveTarget; 
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Target _target)) return;

        RemoveTarget(_target);
    }

    /// <summary>
    /// 타겟 고르기
    /// </summary>
    public bool SelectTarget()
    {
        if (m_targets.Count == 0) return false;

        CurrentTarget = m_targets[0];

        // 타겟 그룹의 멤버 추가
        m_cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    /// <summary>
    /// 타겟 캔슬
    /// </summary>
    public void Cancel()
    {
        if (CurrentTarget == null) return;

        // 타겟 그룹 멤버 제거
        m_cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    /// <summary>
    /// 타겟그룹에서 제거
    /// </summary>
    /// <param name="argTarget">목표</param>
    void RemoveTarget(Target argTarget)
    {
        if (CurrentTarget == argTarget)
        {
            m_cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        argTarget.OnDestoryed -= RemoveTarget; // 타겟에서 Action 할당 제거
        m_targets.Remove(argTarget); // 리스트에서 제거
    }
}
