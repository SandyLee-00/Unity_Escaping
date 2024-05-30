using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInputController inputController;
    private PlayerStatHandler playerStatHandler;

    private Vector3 moveDirection;

    private Vector2 mouseDelta;
    [SerializeField]
    private float mouseSensitivity = 1;
    private float pitch = 0f;
    private float yaw = 0f;
    public float maxPitchAngle = 20f;
    Camera _camera;

    [SerializeField]
    private float jumptForce = 5f;

    private void Awake()
    {
        _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
        inputController = gameObject.GetOrAddComponent<PlayerInputController>();
        playerStatHandler = gameObject.GetOrAddComponent<PlayerStatHandler>();
    }

    void Start()
    {
        inputController.OnMoveEvent += Move;
        inputController.OnJumpEvent += Jump;
        inputController.OnLookEvent += Look;

        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
    }
    private void FixedUpdate()
    {
        MoveFixedUpdate(moveDirection);
        LookFixedUpdate(mouseDelta);
    }

    private void LateUpdate()
    {
    }

    private void Move(Vector2 moveInput)
    {
        moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
    }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            _rigidbody.AddForce(Vector3.up * jumptForce, ForceMode.Impulse);
        }
    }

    private void Look(Vector2 mouseDelta)
    {
        this.mouseDelta = mouseDelta;
    }

    private void MoveFixedUpdate(Vector3 moveDirection)
    {
        Vector3 direction = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        Vector3 move = direction * playerStatHandler.CurrentStat.moveSpeed;
        
        _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);
    }
    
    private void LookFixedUpdate(Vector2 mouseDelta)
    {
        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        _camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }
}
