using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController || collision.gameObject.CompareTag("Player"))
        {
            playerController.shouldPerformRaycast = true;
        }

        Debug.Log("Collision with: " + collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController || collision.gameObject.CompareTag("Player"))
        {
            playerController.shouldPerformRaycast = false;
        }

        Debug.Log("Collision with: " + collision.gameObject.name);
    }
}
