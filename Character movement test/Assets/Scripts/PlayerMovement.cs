using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    [SerializeField]
    private bool isTouchingGround;
    [SerializeField]
    private bool isTouchingItem;
    public float groundCheckRadius;

    [SerializeField]
    private bool isJumping;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask itemLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingItem = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, itemLayer);

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Terrain") || other.gameObject.CompareTag("Item"))
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        if(isTouchingGround || isTouchingItem && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
        }
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

}