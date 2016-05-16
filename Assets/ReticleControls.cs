﻿using UnityEngine;
using System.Collections;

public class ReticleControls : MonoBehaviour
{
    public float MoveSpeed;

    private void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        var verticalMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, verticalMovement);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneReference.PlayerSpawner.Spawn(transform);
            Destroy(gameObject);
        }
    }

}
