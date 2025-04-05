using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; // How much time the player can hang in the air before jumping
    private float coyoteCounter; // How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; // Horizontal wall jump force
    [SerializeField] private float wallJumpY; // Vertical wall jump force

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
   
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip character based on direction
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0); // Set run animation
        anim.SetBool("grounded", isGrounded());   // Set grounded state

        // Set isJumping parameter
        if (isGrounded()) 
        {
            anim.SetBool("isJumping", false); // If grounded, reset jump animation
        }
        else
        {
            // Set isJumping to true if we're in the air and moving upwards (jumping)
            anim.SetBool("isJumping", body.velocity.y > 0); 
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        // Handle wall behavior
        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // If grounded, reset counters
            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        if ((coyoteCounter > 0 || isGrounded()) || jumpCounter > 0)
        {
            SoundManager.instance.PlaySound(jumpSound);

            if (onWall())
            {
                WallJump();
            }
            else
            {
                // Standard ground or air jump
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                if (!isGrounded()) 
                {
                    if (coyoteCounter > 0) 
                        coyoteCounter = 0;
                    else
                        jumpCounter--;
                }
            }

            // Reset counters after jump
            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        // Allow attack if not on wall and grounded or in the air
        return !onWall();  // Attack is allowed in the air as long as not on a wall
    }

    public CoinManager cm;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            cm.coinCount++;
        }
    }
}


