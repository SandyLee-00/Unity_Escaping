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

    private void Awake()
    {
        _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
        inputController = gameObject.GetOrAddComponent<PlayerInputController>();
    }

    void Start()
    {
        inputController.OnMoveEvent += Move;
        inputController.OnJumpEvent += Jump;
        inputController.OnLookEvent += Look;
        
    }

    private void Move(Vector2 moveInput)
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            _rigidbody.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }

    private void Look(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void MoveFixedUpdate(Vector3 moveDirection)
    {
        Vector3 move = moveDirection * playerStatHandler.CurrentStat.moveSpeed;
        _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);
    }

    private void FixedUpdate()
    {
        MoveFixedUpdate(moveDirection);
    }

}
