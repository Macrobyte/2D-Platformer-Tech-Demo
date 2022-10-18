*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField][Range(1f, 10f)] float horizontalMovementSpeed;
    [SerializeField][Range(1f, 10f)] float verticalMovementSpeed;
    [SerializeField][Range(1f, 10f)] float jumpingForce;
    [SerializeField][Range(0.01f, 1f)] float boxCastOffset;
    [SerializeField] LayerMask floorLayer;

    public bool isLadder { get; private set; }
    public bool isClimbingUp { get; private set; }
    public bool isClimbingDown { get; private set; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController == null)
            throw new System.Exception("Couldn't Find Player Controller Script!");
    }

    void Update()
    {
        IsGrounded();  
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Climb();
    }

    void Move()
    {
        playerController.rigidBody2D.velocity = new Vector2(playerController.HorizontalInput * horizontalMovementSpeed, playerController.rigidBody2D.velocity.y);
    }
    
    void Jump()
    {
        if (playerController.JumpInput && IsGrounded())
        {
            playerController.rigidBody2D.velocity = new Vector2(playerController.rigidBody2D.velocity.x, jumpingForce);
        }
    }

    void Climb()
    {
        if(isClimbingUp || isClimbingDown)
        {
            playerController.rigidBody2D.gravityScale = 0f;
            playerController.rigidBody2D.velocity = new Vector2(playerController.rigidBody2D.velocity.x, playerController.VerticalInput * verticalMovementSpeed);
            
        }
    }
    
    public bool IsGrounded(bool debug = false)
    {
        if(debug)
        {
            RaycastHit2D boxCastHit = Physics2D.BoxCast(playerController.boxCollider2D.bounds.center, playerController.boxCollider2D.bounds.size, 0f, Vector2.down, boxCastOffset, floorLayer);

            Color lineColor;

            if (boxCastHit.collider != null)
                lineColor = Color.green;
            else
                lineColor = Color.red;

            Debug.DrawRay(playerController.boxCollider2D.bounds.center + new Vector3(playerController.boxCollider2D.bounds.extents.x, 0), Vector2.down * (playerController.boxCollider2D.bounds.extents.y + boxCastOffset), lineColor);
            Debug.DrawRay(playerController.boxCollider2D.bounds.center - new Vector3(playerController.boxCollider2D.bounds.extents.x, 0), Vector2.down * (playerController.boxCollider2D.bounds.extents.y + boxCastOffset), lineColor);
            Debug.DrawRay(playerController.boxCollider2D.bounds.center - new Vector3(playerController.boxCollider2D.bounds.extents.x, playerController.boxCollider2D.bounds.extents.y + boxCastOffset), Vector2.right * (playerController.boxCollider2D.bounds.size.x), lineColor);

        }
        
        return Physics2D.BoxCast(playerController.boxCollider2D.bounds.center, playerController.boxCollider2D.bounds.size, 0f, Vector2.down, boxCastOffset, floorLayer);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
            isLadder = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
            if(isLadder)
                if (playerController.VerticalInput > 0)
                {
                    isClimbingUp = true;
                    isClimbingDown = false;
                    
                    transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                }
                else if (playerController.VerticalInput < 0)
                {
                    isClimbingDown = true;
                    isClimbingUp = false;
                }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbingUp = false;
            isClimbingDown = false;
            playerController.rigidBody2D.gravityScale = playerController.currentGravityScale;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpperLadder"))
            if(isClimbingUp)
                collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 0;         
            else if (isClimbingDown)
                collision.gameObject.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
    }

}

