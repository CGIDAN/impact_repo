using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorSpawn : MonoBehaviour

{
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

   //initialise
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
    }
}
