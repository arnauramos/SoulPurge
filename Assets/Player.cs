using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 Movement = new Vector2(0f,0f);
    private Rigidbody2D Rigidbody;
    private float delta;
    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime * 1000;
        PlayerMovement();
        PlayerAim();
    }

    void PlayerMovement()
    {
        Movement = new Vector2(0f,0f);
        if (Input.GetKey(KeyCode.W)) Movement.y = Speed * delta;
        if (Input.GetKey(KeyCode.A)) Movement.x = -Speed * delta;
        if (Input.GetKey(KeyCode.S)) Movement.y = -Speed * delta;
        if (Input.GetKey(KeyCode.D)) Movement.x = Speed * delta;
        if (Input.GetKey(KeyCode.LeftShift)) Movement *= 2f;
        Rigidbody.velocity = Movement;
    }
    void PlayerAim()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.up = mousePosition;
    }
}
