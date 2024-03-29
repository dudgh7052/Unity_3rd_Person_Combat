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

    private Camera m_mainCamera;

    /// <summary>
    /// 타겟 리스트
    /// </summary>
    private List<Target> m_targets = new List<Target>();

    /// <summary>
    /// 현재 타겟
    /// </summary>
    public Target CurrentTarget { get; private set; }

    void Start()
    {
        m_mainCamera = Camera.main;
    }

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

        Target _closestTarget = null; // 제일 가까운 타겟 변수
        float _closestTargetDistance = Mathf.Infinity; // 제일 가까운 타겟의 거리

        foreach (Target target in m_targets)
        {
            // target이 카메라 Viewport 밖에 있는지 체크 x, y가 0보다 작을 경우와 1보다 클 경우 Viewport 밖 나타냄
            Vector2 _viewPos = m_mainCamera.WorldToViewportPoint(target.transform.position);

            if (_viewPos.x < 0 || _viewPos.x > 1 || _viewPos.y < 0 || _viewPos.y > 1) continue;

            // camera의 센터(0.5f, 0.5f)에서의 거리 구하기
            Vector2 _toCenter = _viewPos - new Vector2(0.5f, 0.5f);

            // 제일 가까운지 체크
            if(_toCenter.sqrMagnitude < _closestTargetDistance)
            {
                _closestTarget = target;
                _closestTargetDistance = _toCenter.sqrMagnitude;
            }
        }

        if (_closestTarget == null) return false;

        CurrentTarget = _closestTarget;
        m_cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f); // 타겟 그룹의 멤버 추가

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
