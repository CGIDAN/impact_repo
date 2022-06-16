using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public Rigidbody2D rd;
    public float speed;
    float spawnDistance = 10;
    Transform planet = null;

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // this for explosion later! Instantiate(explosion,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
