using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    public delegate void DoorDelegate();
    public DoorDelegate onOpen;


    public void Open()
    {
        animator.SetTrigger("Open");
        onOpen?.Invoke();

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
