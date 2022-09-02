using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public delegate void PuzzleResolved();
    public PuzzleResolved onResolved;

    public abstract void Resolved();
}
