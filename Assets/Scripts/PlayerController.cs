using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //start() variables 
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;


    //FSM
    private enum State { idle, running, jumping, falling, hurt}
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private int gem = 0;
    [SerializeField] private Text gemText;
    [SerializeField] private float hurtForce = 10f;

    private void Start()

  
     {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
     }
        
           
       

    private void Update()
    {
        Movement();

        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state
    }

    private void Movement()
    {
        if (state != State.hurt)
        {

        }
        //moving left
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //moving right
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            gem += 1;
            gemText.text = gem.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is too my right therefor i should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Enemy is too my left and therefor i should be damage dand move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
            
        }

    }

    private void AnimationState()
    {
        if(state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            //moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
      
    }



}

