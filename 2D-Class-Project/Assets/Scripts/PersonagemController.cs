using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float vel;
    public float jumpForce;
    public GameObject groundCheck;
    private GroundCheck groundCheckScript;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        groundCheckScript = groundCheck.GetComponent<GroundCheck>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(rb2d.velocity.magnitude<5){
            rb2d.velocity += new Vector2(vel,0)*horizontalInput*Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && groundCheckScript.isOnGround)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,jumpForce);
        }
    }
}
