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

    private Camera m_mainCamera;

    /// <summary>
    /// Ÿ�� ����Ʈ
    /// </summary>
    private List<Target> m_targets = new List<Target>();

    /// <summary>
    /// ���� Ÿ��
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

        Target _closestTarget = null; // ���� ����� Ÿ�� ����
        float _closestTargetDistance = Mathf.Infinity; // ���� ����� Ÿ���� �Ÿ�

        foreach (Target target in m_targets)
        {
            // target�� ī�޶� Viewport �ۿ� �ִ��� üũ x, y�� 0���� ���� ���� 1���� Ŭ ��� Viewport �� ��Ÿ��
            Vector2 _viewPos = m_mainCamera.WorldToViewportPoint(target.transform.position);

            if (_viewPos.x < 0 || _viewPos.x > 1 || _viewPos.y < 0 || _viewPos.y > 1) continue;

            // camera�� ����(0.5f, 0.5f)������ �Ÿ� ���ϱ�
            Vector2 _toCenter = _viewPos - new Vector2(0.5f, 0.5f);

            // ���� ������� üũ
            if(_toCenter.sqrMagnitude < _closestTargetDistance)
            {
                _closestTarget = target;
                _closestTargetDistance = _toCenter.sqrMagnitude;
            }
        }

        if (_closestTarget == null) return false;

        CurrentTarget = _closestTarget;
        m_cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f); // Ÿ�� �׷��� ��� �߰�

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
