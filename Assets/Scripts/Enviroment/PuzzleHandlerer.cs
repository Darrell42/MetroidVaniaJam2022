using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandlerer : Puzzle
{

    [SerializeField]
    private List<Puzzle> puzzleList;
    private int countResolvedPuzzles = 0;

    //[SerializeField]
    //private GameObject door;

    [SerializeField]
    private UnityEvent onCompleteEvent;

    //public delegate void PuzzleDelegate();
    //public PuzzleDelegate onResolved;



    public override void Resolved()
    {
        onResolved?.Invoke();
        //door.SetActive(false);
        onCompleteEvent?.Invoke();
    }

    private void DelegatedPuzzleReolved()
    {
        countResolvedPuzzles++;
        if(countResolvedPuzzles >= puzzleList.Count)
        {
            Resolved();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        foreach(Puzzle puzzle in puzzleList)
        {
            puzzle.onResolved += DelegatedPuzzleReolved;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
