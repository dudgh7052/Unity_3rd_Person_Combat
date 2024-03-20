using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController m_controller;

    private float m_verticalGravity;

    // Property
    public Vector3 Movement => Vector3.up * m_verticalGravity;

    void Update()
    {
        if (m_verticalGravity < 0.0f && m_controller.isGrounded)
        {
            m_verticalGravity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            m_verticalGravity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
