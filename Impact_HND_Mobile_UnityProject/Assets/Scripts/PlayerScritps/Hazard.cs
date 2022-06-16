using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Built-in Unity function for handling collisions
    // This function will be called when another object bumps 
    // into the one this script is attached to
    void OnCollisionEnter2D(Collision2D collisionData)
    {
        // Get the object we collided with
        Collider2D objectWeCollidedWith = collisionData.collider;

        // Get the PlayerHealth script attached to that object (if there is one)
        PlayerHealth player = objectWeCollidedWith.GetComponent<PlayerHealth>();

        // Check if we actually found a player health script
        // This if statement is true if the player variable is NOT null (empty)
        if (player != null)
        {
            // This means there WAS a PlayerHealth script attached to the object we bumped into
            // Which means this object is indeed the player

            // Perform our on-collision action (kill the player)
            player.Kill();
        }
    }
}