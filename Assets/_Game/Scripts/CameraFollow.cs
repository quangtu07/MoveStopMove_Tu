using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] Transform tf;
    private Transform playerTranform;
    [SerializeField] private Vector3 offset;
    private Vector3 initialPosition; // Variable to store the initial position of the camera

    private void Awake()
    {
        // Store the initial position of the camera at the start
        initialPosition = tf.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (playerTranform != null)
        {
            tf.position = playerTranform.position + offset;
        }
    }

    private void CalculateOffset()
    {
        if (playerTranform != null)
        {
            offset = tf.position - playerTranform.position;
        }
    }

    public void SetPlayer(Player currentPlayer)
    {
        playerTranform = currentPlayer.Tf;
        tf.position = initialPosition;
        CalculateOffset(); // Recalculate offset whenever a new player is set
    }

    public void RemovePlayer()
    {
        playerTranform = null; // Remove the player reference
    }
}
