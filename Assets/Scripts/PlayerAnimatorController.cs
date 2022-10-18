using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerMovement playerMovement;

    public enum AnimationLayers
    {
        GroundLayer = 0,
        AirLayer = 1,
        ClimbLayer = 2
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
            throw new System.Exception("Couldn't Find Player Controller Script!");

        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
            throw new System.Exception("Couldn't Find Player Movement Script!");
    }
    
    void Update()
    {
        playerController.animator.SetFloat("HorizontalSpeed", Mathf.Abs(playerController.HorizontalInput));
        
        playerController.animator.SetFloat("VerticalSpeed", Mathf.Abs(playerController.VerticalInput));

        
        if (playerMovement.IsGrounded())
        {
            playerController.animator.SetBool("IsGrounded", true);
            playerController.animator.speed = 1f;
        }                                            
        else
            playerController.animator.SetBool("IsGrounded", false);

        if (playerMovement.isClimbingUp || playerMovement.isClimbingDown)
        {
            playerController.animator.SetBool("IsClimbing", true);   
            
            if (playerController.VerticalInput == 0)
                playerController.animator.speed = 0f;
            else
                playerController.animator.speed = 1f;

        }
        else
            playerController.animator.SetBool("IsClimbing", false);
        
        
        if (playerController.rigidBody2D.velocity.normalized.y > 0 && playerController.rigidBody2D.gravityScale == playerController.currentGravityScale)
        {
            playerController.animator.SetBool("IsJumping", true);
            playerController.animator.SetBool("IsFalling", false);
            
        }
        else if (playerController.rigidBody2D.velocity.normalized.y < 0 && playerController.rigidBody2D.gravityScale == playerController.currentGravityScale)
        {
            playerController.animator.SetBool("IsJumping", false);
            playerController.animator.SetBool("IsFalling", true);
            
        }
        else
        {
            playerController.animator.SetBool("IsJumping", false);
            playerController.animator.SetBool("IsFalling", false);
        }


        if (playerController.attackInput)
        {
            playerController.animator.SetBool("IsAttacking", true);
        }
        else
        {
            playerController.animator.SetBool("IsAttacking", false);
        }

    }       
}

