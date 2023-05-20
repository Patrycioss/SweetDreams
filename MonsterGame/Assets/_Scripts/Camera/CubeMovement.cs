using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;

    private InputActionMap _inputActionMap;

    private InputAction _inputActionJump, _inputActionMoveForward, _inputActionMoveBackward, _inputActionMoveLeft, _inputActionMoveRight;

    private Rigidbody _rb;

    private void Awake()
    {
        _inputActionMap = _inputActionAsset.FindActionMap("Player1");
        _inputActionMap.Enable();
        _inputActionJump = _inputActionMap.FindAction("jump");
        _inputActionMoveForward = _inputActionMap.FindAction("move_forward");
        _inputActionMoveBackward = _inputActionMap.FindAction("move_backward");
        _inputActionMoveLeft = _inputActionMap.FindAction("move_left");
        _inputActionMoveRight = _inputActionMap.FindAction("move_right");
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputActionJump.performed += (context => _rb.AddForce(new Vector3(0, 1000, 0)));
    }

    private void FixedUpdate()
    {
        if (_inputActionMoveForward.IsPressed())
        {
            _rb.AddForce(new Vector3(-20, 0, 0));
        }
        if (_inputActionMoveBackward.IsPressed())
        {
            _rb.AddForce(new Vector3(20, 0, 0));
        }
        if (_inputActionMoveLeft.IsPressed())
        {
            _rb.AddForce(new Vector3(0, 0, -20));
        }
        if (_inputActionMoveRight.IsPressed())
        {
            _rb.AddForce(new Vector3(0, 0, 20));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        Debug.Log(Input.GetAxis("Vertical"));
    }
}
