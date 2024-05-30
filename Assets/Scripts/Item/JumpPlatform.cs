using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private GameObject _player;

    private PlayerMovement _playerMovement;

    [SerializeField]
    private float jumpForce = 10f;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerMovement.JumpByOther(jumpForce);
        }
    }
}
