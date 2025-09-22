using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class ExplorerScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) move = Vector2.up;
        if (Input.GetKey(KeyCode.S)) move = Vector2.down;
        if (Input.GetKey(KeyCode.A)) move = Vector2.left;
        if (Input.GetKey(KeyCode.D)) move = Vector2.right;

        myRigidBody.linearVelocity = move * moveSpeed;
        Debug.Log(myRigidBody.linearVelocity);
    }

}
