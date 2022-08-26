using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;


    [Header("Skill")]

    [SerializeField] private BaseSkill jumpSkill;
    public BaseSkill JumpSkill { get { return jumpSkill; } }


    [SerializeField] private BaseSkill doubleJumpSkill;
    public BaseSkill DoubleJumpSkill { get { return doubleJumpSkill; } }
    //public BaseSkill jumpSkill2;

    [SerializeField] private BaseSkill pushBox;
    public BaseSkill PushBox { get { return pushBox; } }
    //public BaseSkill jumpSkill2;

    #region SingleTon

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject newManager = new GameObject("GameManager");
                newManager.AddComponent<GameManager>();
                
            }
            return instance;

        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

}
