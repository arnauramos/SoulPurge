using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 Movement = new Vector2(0f,0f);
    private Rigidbody2D rb2d;
    //private float delta;
    private float fixedDelta;
    public float Speed = 5f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        fixedDelta = Time.fixedDeltaTime * 1000;
        PlayerMovement();
        PlayerAim();
    }

    void Update()
    {
        //delta = Time.deltaTime * 1000;     
    }

    void PlayerMovement()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) Speed = 10f;
        else Speed = 5f; 
        rb2d.velocity = Movement * Speed * fixedDelta;
    }
    void PlayerAim()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 lookDirection = mousePosition - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }
}
