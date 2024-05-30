using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<float> OnLookEvent;
    public event Action OnJumpEvent;

    private Camera _camera;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        // Ensure the Action Map is enabled
        _playerInput.actions.Enable();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");

        Vector2 moveInput = value.Get<Vector2>().normalized;

        OnMoveEvent?.Invoke(moveInput);
    }

    /// <summary>
    /// 플레이어 -> 마우스 방향 벡터를 구하는 함수
    /// </summary>
    /// <param name="value"></param>
    public void OnLook(InputValue value)
    {
        Debug.Log("OnLook");

        Vector2 mousePosition = value.Get<Vector2>();

        if (_camera == null)
        {
            Debug.LogError("Main Camera is not found");
            return;
        }

        Vector3 mouseInWorldPosition = _camera.ScreenToWorldPoint(mousePosition);

        
        Vector3 lookDirection = mouseInWorldPosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;

        OnLookEvent?.Invoke(angle);
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("OnJump");
        OnJumpEvent?.Invoke();
    }

}
