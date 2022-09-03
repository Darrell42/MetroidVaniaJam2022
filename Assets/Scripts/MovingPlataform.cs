using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    public bool activated;

    private PlayerUnit currentPlayer;

    private Rigidbody plataform;

    [SerializeField]
    private MovingPlataformTarget firstTarget;


    [SerializeField]
    private float speed = 0.5f;

    public Vector3 targetDirection;


    public void SetActivated(bool active)
    {
        activated = active;
    }

    public void SetDestination(Vector3 destination)
    {
        Vector3 destinationDirection= new Vector3(0f, destination.y, destination.z);
        Vector3 plataformDirection = new Vector3(0f, plataform.transform.position.y, plataform.transform.position.z);

        targetDirection = (destinationDirection - plataformDirection).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        plataform = GetComponent<Rigidbody>();
        SetDestination(firstTarget.transform.position);
    }

    private void FixedUpdate()
    {
        if (activated && targetDirection != null)
        {
            plataform.MovePosition( plataform.transform.position + targetDirection * speed * Time.fixedDeltaTime);


            if (currentPlayer != null)
            {
                currentPlayer.GetComponent<CharacterController>().Move(targetDirection * speed * Time.fixedDeltaTime);
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();

        if (player != null)
        {
            //player.transform.parent = transform;
            currentPlayer = player;        }

    }

    private void OnTriggerExit(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();

        if (player != null)
        {
            //player.transform.parent = null;
            currentPlayer = null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerUnit player =  collision.collider.GetComponent<PlayerUnit>();

        if (player != null)
        {
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerUnit player = collision.collider.GetComponent<PlayerUnit>();

        if (player != null)
        {
            player.transform.parent = null;
        }

    }
}
