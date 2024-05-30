using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    private Vector3 positionOffset = new Vector3(0, 4, -3);
    [SerializeField]
    private Vector3 rotationOffset = new Vector3(30, 0, 0);


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerState");
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        if (player.IsValid() == false)
        {
            return;
        }

    }
}
