using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 0.2f;
    public float jumpForce = 5000f;
    private SpriteRenderer spriteRenderer;
    public bool isGround = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") > 0 && isGround)
        {
            isGround = false;
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
        }


        float translation = Input.GetAxis("Horizontal") * speed;
        transform.Translate(translation, 0, 0);
        if(translation > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(translation < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)// 8 is ground
        {
            isGround = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)// 8 is ground
        {
            isGround = true;
        }
    }
}
