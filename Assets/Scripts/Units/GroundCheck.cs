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
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerUnit>() == null)
        {
            grounded = false;
        }
    }

}
