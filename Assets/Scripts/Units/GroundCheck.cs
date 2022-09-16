using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private bool grounded;

    public bool IsGrounded()
    {
        return grounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerUnit>() == null)
        {
            //grounded = true;
            float velocity = GameManager.Instance.Player.GetComponent<PlayerMovement>().velocityInY;
            Debug.Log(velocity);

            if(velocity <= -15f)
            {
                GameManager.Instance.Player.GetComponent<Animator>().SetTrigger("Die");
                return;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerUnit>() == null)
        {
            grounded = false;
        }
    }

    //

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerUnit>() == null)
        {
            grounded = true;
        }

    }


}
