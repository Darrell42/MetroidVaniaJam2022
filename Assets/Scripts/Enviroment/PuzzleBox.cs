using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBox : Puzzle
{
    public bool puzzleResolved;

    //public  delegate void PuzzleResolved();
    //public  PuzzleResolved onResolved;

    public override void Resolved()
    {
        onResolved?.Invoke();
        puzzleResolved = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.collider.GetComponent<Box>();
        if(box != null)
        {
            Resolved();
        }
    }

}
