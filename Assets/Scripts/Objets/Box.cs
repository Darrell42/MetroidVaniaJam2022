using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IPusheable, IDamagable
{

    private Rigidbody rigidbody;

    private bool canPush = true;

    private bool useFakeGravity = false;

    [SerializeField]
    private float fakeForce = 5.3f;


    public void ApplyDamage(float damage)
    {
        Debug.Log("Hit: " + damage);

    }

    public void ApplyDamage(float damage, Vector3 direction)
    {

    }

public bool CanPush()
    {
        return canPush;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        //if(originalPsition == null) originalPsition.position = transform.position;

    }

    private void FixedUpdate()
    {
        if (rigidbody.useGravity && useFakeGravity)
        {

            //rigidbody.AddForce(new Vector3(0f, gravity * rigidbody.mass * Time.fixedDeltaTime, 0f));

            if (rigidbody.velocity.magnitude > 0)
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y * rigidbody.mass * Time.fixedDeltaTime, rigidbody.velocity.z);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Puzzle isPuzzle = collision.collider.GetComponent<Puzzle>();
        Wall isWall = collision.collider.GetComponent<Wall>();

        canPush = (isPuzzle == null && isWall == null);
    }

    private void OnCollisionExit(Collision collision)
    {
        Puzzle isPuzzle = collision.collider.GetComponent<Puzzle>();
        Wall isWall = collision.collider.GetComponent<Wall>();

        canPush = !(isPuzzle == null && isWall == null);
    }

}
