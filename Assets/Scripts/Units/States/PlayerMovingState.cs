using UnityEngine;

public class PlayerMovingState : PlayerStateBase
{

    private PlayerMovement playerMovement;
    private Animator animator;


    public override void EnterState(PlayerUnit player)
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = player.GetComponent<Animator>();

        player.curentStateNae = "Moving";
        
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

    public override void OnTriggerEnter(PlayerUnit player, Collider other)
    {
        
    }

    public override void OnTriggerExit(PlayerUnit player, Collider other)
    {
        
    }

    public override void Update(PlayerUnit player)
    {
        //Change to airbone State
        if (!playerMovement.grounded)
        {
            player.TransitionToState(player.playerAirBoneState);
        }

        //jump
        if (player.controls.Gameplay.Jump.triggered && player.FindSkill(GameManager.Instance.JumpSkill))
        {
            playerMovement.Jump();
        }
       
    }


}
