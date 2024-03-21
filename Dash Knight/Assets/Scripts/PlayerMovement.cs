using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Security.Cryptography;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_RigidBody;
    public float moveSpeed = 10f;
    public float jumpForce = 5f;

    private float horizontal;
    private int playerScore;
    private UIManager _UIManager;

    private bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;


    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private AudioSource dashAudio;
    [SerializeField] private AudioSource coindAudio;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerScore = 0;
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_UIManager == null)
        {
            Debug.Log("Errror Script Not Found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        m_RigidBody.velocity = new Vector2(horizontal * moveSpeed, m_RigidBody.velocity.y);
    }

    //Handles collision with enemies and coins
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (isDashing)
            {
                Destroy(collision.gameObject);
                playerScore += 100;
                _UIManager.UpdateScore(playerScore);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if(collision.gameObject.CompareTag(("Coin")))
        {
            playerScore += 50;
            _UIManager.UpdateScore(playerScore);
            coindAudio.Play();
            Destroy(collision.gameObject);
        }
        
    }

    //Handles collision with the win area. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Win"))
        {
            _UIManager.ShowWinText();
        }
    }

    //Gets the horizontal velocity from the user input
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        Flip();
    }

    //Checks if the player is in contact with the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Makes the player character jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (!isDashing)
        {
            if (context.performed && IsGrounded())
            {
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, jumpForce);
            }

            if (context.canceled && m_RigidBody.velocity.y > 0f)
            {
                m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, m_RigidBody.velocity.y * 0.5f);
            }
        }
    }

    //Calls the dash animation
    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            dashAudio.Play();
            StartCoroutine(DashRoutine());
        }
    }

    //Handles the player dash movement 
    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;
        float originalGrav = m_RigidBody.gravityScale;
        m_RigidBody.gravityScale = 0f;
        m_RigidBody.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        m_RigidBody.gravityScale = originalGrav;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    //Flips the players direction based on the direction of movement
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
