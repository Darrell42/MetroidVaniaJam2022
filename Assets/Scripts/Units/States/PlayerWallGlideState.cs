using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGlideState : PlayerStateBase
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private float jumpCooldown = 10f;

    private bool canJump = true;

    private bool sliding;



    public override void EnterState(PlayerUnit player)
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();

        player.curentStateNae = "Wall Jump";

        sliding = true;

        playerMovement.velocityInY = 0f;
    }

    public override void FixedUpdate(PlayerUnit player)
    {
        playerMovement.ApplayGravity(-1f);
        //playerMovement.Move2D(new Vector3(0f, 0f, player.moveInput.x));
        playerMovement.Move(new Vector3(player.moveInput.x, 0f, 0f));

        animator.SetBool("WallSlide", sliding);
        if (!sliding)
        {
            animator.SetBool("Airbone", true);
            player.TransitionToState(player.playerAirBoneState);
        }

        sliding = false;


    }

    public override void OnCollisionEnter(PlayerUnit player, Collision collision)
    {

    }

    public override void OnCollisionExit(PlayerUnit player, Collision collision)
    {

    }

    public override void OnControllerColliderHit(PlayerUnit player, ControllerColliderHit hit)
    {

        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        bool hasWallJump = player.FindSkill(GameManager.Instance.WallJump);

        Rigidbody body = hit.collider.attachedRigidbody;

        Islidable slideable = hit.collider.GetComponent<Islidable>();

        // no rigidbody
        if (body == null || !hasWallJump || slideable == null)
        {
            return;
        }

        sliding = true;

    }

    public override void OnTriggerEnter(PlayerUnit player, Collider other)
    {

    }

    public override void OnTriggerExit(PlayerUnit player, Collider other)
    {

    }

    public override void Update(PlayerUnit player)
    {

        if (playerMovement.grounded)
        {

            animator.SetBool("Airbone", false);
            animator.SetBool("WallSlide", false);
            player.TransitionToState(player.playerMovingState);
        }

        //jump on button trigger, the findskill was moved inside the first fi satetment so it doesent called in every update
        if (player.controls.Gameplay.Jump.triggered)
        {
            bool hasJumpSkill = player.FindSkill(GameManager.Instance.WallJump);

            //probably an animation will be added
            if (hasJumpSkill && canJump)
            {
                playerMovement.WallJump();
                
                animator.SetBool("WallSlide", false);

                //player.StartCoroutine("SkillCoolDown", 2f, GameManager.Instance.WallJump);
                //player.StartCoroutine(player.SkillCoolDown(0.7f, GameManager.Instance.DoubleJumpSkill));
                if(playerMovement.wallJumpCoolDown > 0)
                    player.StartCoroutine(player.SkillCoolDown(playerMovement.wallJumpCoolDown, GameManager.Instance.WallJump));

                player.TransitionToState(player.playerAirBoneState);

                player.countJump++;

                animator.SetTrigger("Jump");

            }
        }

        

    }
}
