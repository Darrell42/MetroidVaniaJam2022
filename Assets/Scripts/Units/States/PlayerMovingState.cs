using UnityEngine;

public class PlayerMovingState : PlayerStateBase
{

    private PlayerMovement playerMovement;
    private Animator animator;
    private bool pushing = false;


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

        //This is used because OncolliderHit has no exit ecent
        animator.SetBool("Push", pushing);
        if (!pushing) player.GetComponent<CharacterController>().radius = 0.13f;
        pushing = false;
    }

    public override void OnCollisionEnter(PlayerUnit player, Collision collision)
    {
        
    }

    public override void OnCollisionExit(PlayerUnit player, Collision collision)
    {

    }

    public override void OnControllerColliderHit(PlayerUnit player, ControllerColliderHit hit)
    {

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        bool haspushBoxSkill = player.FindSkill(GameManager.Instance.PushBox);

        Rigidbody body = hit.collider.attachedRigidbody;

        IPusheable pusheable  = hit.collider.GetComponent<IPusheable>();

        // no rigidbody
        if (body == null || body.isKinematic || !haspushBoxSkill || pusheable == null)
        {
            return;
        }

        pushing = true;

        if (player.GetComponent<CharacterController>().radius < 0.5f) player.GetComponent<CharacterController>().radius += 1f * Time.deltaTime;

        //player.GetComponent<CharacterController>().radius = 0.50f;
        //animator.SetBool("Push", true);

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
            animator.SetBool("Airbone", true);
            player.TransitionToState(player.playerAirBoneState);
        }

        
        //jump on button trigger, the findskill was moved inside the first fi satetment so it doesent called in every update
        if (player.controls.Gameplay.Jump.triggered)
        {
            bool hasJumpSkill = player.FindSkill(GameManager.Instance.JumpSkill);

            //probably an animation will be added
            if (hasJumpSkill) 
            {
                playerMovement.Jump();
            }
        }
       
    }


}
