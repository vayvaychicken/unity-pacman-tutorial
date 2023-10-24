using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball_Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    public float moveSpeed = 5f;
    public float jumpForce = 6f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool grounded = IsGrounded();
        dirX = Input.GetAxisRaw("Horizontal");
        float xVelocity = dirX * moveSpeed;
        if (!grounded) { xVelocity /= 3; }
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            if (jumpSoundEffect != null) { jumpSoundEffect.Play(); }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
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

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}