using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreckablePuzzle : Puzzle
{

    public bool puzzleResolved;

    private Breckable_Objet breckable;

    public override void Resolved()
    {
        onResolved?.Invoke();
        puzzleResolved = true;
}

    // Start is called before the first frame update
    void Start()
    {
        breckable = GetComponent<Breckable_Objet>();

        if(breckable != null)
        {
            breckable.onBreck += Resolved;
        }
    }

}
