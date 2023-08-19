using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField] private float moveAcceleration = 60f;
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] float jumpforce = 7f;
    [SerializeField] private LayerMask jumpableground;
    [SerializeField] private int maxJumpTimes;
    private int jumpTimesRemaining;
    float dirX;

    [SerializeField] private GameObject Gun;


    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
/*        jumpTimesRemaining = maxJumpTimes;*/
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && jumpTimesRemaining > 0)
        {
            jumpTimesRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
        else
        {
            if (IsGround())
            {
                jumpTimesRemaining = maxJumpTimes;
            }
        }

    }

    private void FixedUpdate()
    {


        dirX = Input.GetAxis("Horizontal");

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            Gun.transform.position = new Vector2(this.transform.position.x + .8f, Gun.transform.position.y);

        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
            Gun.transform.position = new Vector2(this.transform.position.x - .8f, Gun.transform.position.y);
        }
        else
        {
            state = MovementState.idle;
        }
         
        if (rb.velocity.y > .1f)
        {

            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        float targetVelocity = dirX * maxMoveSpeed;
        float acceleration = Mathf.Abs(rb.velocity.x - targetVelocity) < 0.1f ? 0f : moveAcceleration;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, targetVelocity, acceleration * Time.deltaTime), rb.velocity.y);
        animator.SetInteger("state",StateReturn());
    }



    private bool IsGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableground);
    }


    private int StateReturn()
    {
        if (state == MovementState.running)
        {
            return 1;
        }
        else if (state == MovementState.idle)
        {
            return 0;
        }
        else if (state == MovementState.falling)
        {
            return 3;
        }
        else 
        { 
            return 2; 
        }
    }
}
