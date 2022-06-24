using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerInput;
    private PlayerControls.ControlsActions onFoot;

    private Player_MAIN motor;
    private SkillManager SM;
    private Look look;
    public ZiplaneActivator ZLA;
    private void Awake()
    {
        playerInput = new PlayerControls();
        onFoot = playerInput.Controls;
        motor = GetComponent<Player_MAIN>();
        SM = GetComponent<SkillManager>();
        look = GetComponent<Look>();
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    private void FixedUpdate()
    {
        if (!GetComponent<PlayerSTAT>().Dead)
        {
            motor.Move(onFoot.Movement.ReadValue<Vector2>());
        }
    }

    private void LateUpdate()
    {
        if (!motor.InShop)
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
