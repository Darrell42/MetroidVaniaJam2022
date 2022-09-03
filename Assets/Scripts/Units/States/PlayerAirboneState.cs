

using UnityEngine;

public class PlayerAirboneState : PlayerStateBase
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private int countJump;

    private bool sliding;


    public override void EnterState(PlayerUnit player)
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();

        player.curentStateNae = "Airbone";

        countJump = 0;

        player.countJump = 0;

        sliding = false;
    }

    public override void FixedUpdate(PlayerUnit player)
    {
        playerMovement.ApplayGravity();
        //playerMovement.Move2D(new Vector3(0f, 0f, player.moveInput.x));
        playerMovement.Move(new Vector3(player.moveInput.x, 0f, 0f));
    }

    public override void OnCollisionEnter(PlayerUnit player, Collision collision)
    {

    }

    public override void OnCollisionExit(PlayerUnit player, Collision collision)
    {

    }

    public override void OnControllerColliderHit(PlayerUnit player, ControllerColliderHit hit)
    {
        if (hit.moveDirection.y < -0.5)
        {
            return;
        }

        bool hasWallJump = player.FindSkill(GameManager.Instance.WallJump);

        Rigidbody body = hit.collider.attachedRigidbody;

        Islidable slideable = hit.collider.GetComponent<Islidable>();

        // no rigidbody
        if (body == null  || !hasWallJump || slideable == null)
        {
            return;
        }

        if (playerMovement.velocityInY < 0)
            //playerMovement.velocityInY = 0f;
            player.TransitionToState(player.playerSlideableState);

    }

    public override void OnTriggerEnter(PlayerUnit player, Collider other)
    {

    }

    public override void OnTriggerExit(PlayerUnit player, Collider other)
    {

    }

    public override void Update(PlayerUnit player)
    {
        //Change to moving State
        if (playerMovement.grounded)
        {

            animator.SetBool("Airbone", false);
            player.TransitionToState(player.playerMovingState);
        }

        //DoubleJump
        if ((player.controls.Gameplay.Jump.triggered)  &&  (player.countJump < playerMovement.MaxAerialJump) )
        {

            //FindSkill moved inside so is no call on every update
            bool hasDoubleJumpSkill = player.FindSkill(GameManager.Instance.DoubleJumpSkill);

            if (hasDoubleJumpSkill)
            {
                countJump++;
                player.countJump++;
                animator.SetTrigger("Jump");

                if(playerMovement.doubleJumpCoolDown > 0)
                    player.StartCoroutine(player.SkillCoolDown(playerMovement.doubleJumpCoolDown, GameManager.Instance.DoubleJumpSkill));
                playerMovement.Jump();
            }

        }

    }
}
