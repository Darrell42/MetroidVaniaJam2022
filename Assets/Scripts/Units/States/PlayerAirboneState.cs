

using UnityEngine;

public class PlayerAirboneState : PlayerStateBase
{
    private PlayerMovement playerMovement;
    private Animator animator;

    private int countJump;


    public override void EnterState(PlayerUnit player)
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();

        player.curentStateNae = "Airbone";

        countJump = 0;
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
            player.TransitionToState(player.playerMovingState);
        }

        //DoubleJump
        if ((player.controls.Gameplay.Jump.triggered)  &&  (countJump < playerMovement.MaxAerialJump) )
        {

            //FindSkill moved inside so is no call on every update
            bool hasDoubleJumpSkill = player.FindSkill(GameManager.Instance.DoubleJumpSkill);

            if (hasDoubleJumpSkill)
            {
                countJump++;
                playerMovement.Jump();
            }

        }

    }
}
