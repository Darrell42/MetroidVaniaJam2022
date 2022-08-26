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

    public override void OnControllerColliderHit(PlayerUnit player, ControllerColliderHit hit)
    {

        bool haspushBoxSkill = player.FindSkill(GameManager.Instance.PushBox);

        Rigidbody body = hit.collider.attachedRigidbody;
        
        // no rigidbody
        if (body == null || body.isKinematic || !haspushBoxSkill)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * 2f;

    }

    public override void OnTriggerEnter(PlayerUnit player, Collider other)
    {
        
    }

    public override void OnTriggerExit(PlayerUnit player, Collider other)
    {
        
    }

    public override void Update(PlayerUnit player)
    {
        //Change to airbone State when player is no longer rounded
        if (!playerMovement.grounded)
        {
            player.TransitionToState(player.playerAirBoneState);
        }

        
        //jump on button trigger, the findskill was moved inside the first fi satetment so it doesent called in every update
        if (player.controls.Gameplay.Jump.triggered)
        {
            bool hasJumpSkill = player.FindSkill(GameManager.Instance.JumpSkill);
            
            //probably an animation will be added
            if(hasJumpSkill) playerMovement.Jump();
        }
       
    }


}
