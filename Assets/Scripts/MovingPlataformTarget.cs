using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataformTarget : MonoBehaviour
{

    [SerializeField]
    private Transform nextTarget;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        MovingPlataform plataform = other.GetComponent<MovingPlataform>();

        if(plataform != null)
        {

            if(nextTarget != null)
            {
                plataform.SetDestination(nextTarget.position);
            }
            else
            {
                plataform.activated = false;
            }

            
        }
        
    }
}
