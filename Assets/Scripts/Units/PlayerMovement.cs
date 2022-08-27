using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerUnit player;
    private CharacterController characterController;
    private Animator animator;

    public bool grounded = true;

    private float velocityInY = 0f;

    private float distanceToGround = 0;
    [SerializeField] private float maxDistanceY = 0.4f;

    [SerializeField]
    private float speed = 0;

    [SerializeField] 
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [SerializeField]
    private float damptimeMovement = 0.2f;
    
    [SerializeField] 
    private float gravity = -9.8f;

    [SerializeField]
    private float jumpForce = 2f;

    [SerializeField]
    private int maxAerialJump = 1;
    public int MaxAerialJump { get { return maxAerialJump; } }

    [SerializeField] private Transform cam;
    [SerializeField] private Transform gfxRotation;
    [SerializeField] private GroundCheck groundCheck;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        player.GetComponent<PlayerUnit>();
    }

    public void ApplayGravity()
    {
        velocityInY += gravity * Time.fixedDeltaTime;

        DistanceToGround();
        
        if (grounded && velocityInY < 0)
        {
            velocityInY = -1f;
        }
        //grounded = characterController.isGrounded || distanceToGround < maxDistanceY || groundCheck.IsGrounded();
        grounded = characterController.isGrounded || groundCheck.IsGrounded();
    }

    private void DistanceToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            distanceToGround = hit.distance;
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * 1000, Color.red);
        }

    }



    public void Move(Vector3 movement)
    {
        Vector3 direction = movement.normalized;

        //animation Stuff (Animation stuff should be in the state machine but i'm lazy)
        animator.SetFloat("MoveX", direction.x, damptimeMovement, Time.fixedDeltaTime);
        animator.SetFloat("MoveY", direction.z, damptimeMovement, Time.fixedDeltaTime);

        //There is an issue with the damptime where the float value is never 0 this IF is to force it to 0 when there is no movement
        if ((Mathf.Abs(animator.GetFloat("MoveX")) < 0.01f) && (Mathf.Abs(animator.GetFloat("MoveY")) < 0.01f))
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
        }

        if (direction.magnitude > 0)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(gfxRotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            gfxRotation.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            moveDirection.Normalize();

            characterController.Move(new Vector3(0f, velocityInY, moveDirection.z) * speed * Time.fixedDeltaTime);
        }
        else
        {
            //if (!grounedd) 
            characterController.Move(new Vector3(0, velocityInY, 0) * speed * Time.fixedDeltaTime);
        }



    }

    public void Jump()

        {
        //velocityInY += Mathf.Sqrt(jumpForce * -2f * gravity);
        velocityInY = jumpForce;
            grounded = false;
        }

}
