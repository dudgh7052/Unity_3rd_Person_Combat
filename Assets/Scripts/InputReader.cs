using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    public event Action m_jumpEvent;
    public event Action m_dodgeEvent;

    private Controls m_controls;

    void Start()
    {
        m_controls = new Controls();
        m_controls.Player.SetCallbacks(this);

        m_controls.Player.Enable();
    }

    private void OnDestroy()
    {
        m_controls.Player.Disable();
    }

    

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        m_jumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        m_dodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
}
