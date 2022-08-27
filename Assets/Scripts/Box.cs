using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IPusheable
{

    private Rigidbody rigidbody;

    [SerializeField]
    private bool useFakeGravity;

    [SerializeField]
    private float fakeForce = 5.3f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }

    private void FixedUpdate()
    {
        if (rigidbody.useGravity && useFakeGravity)
        {

            //rigidbody.AddForce(new Vector3(0f, gravity * rigidbody.mass * Time.fixedDeltaTime, 0f));

            if(rigidbody.velocity.magnitude > 0)
            rigidbody.velocity =   new Vector3(rigidbody.velocity.x, rigidbody.velocity.y * rigidbody.mass * Time.fixedDeltaTime, rigidbody.velocity.z);





        }
    }
}
