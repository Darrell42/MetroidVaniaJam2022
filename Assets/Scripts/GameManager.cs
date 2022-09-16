using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField] private PlayerUnit player;
    public PlayerUnit Player { get { return player; } }

    public GameObject cameraFallow;

    [Header("Skill")]

    [SerializeField] private BaseSkill jumpSkill;
    public BaseSkill JumpSkill { get { return jumpSkill; } }


    [SerializeField] private BaseSkill doubleJumpSkill;
    public BaseSkill DoubleJumpSkill { get { return doubleJumpSkill; } }
    //public BaseSkill jumpSkill2;

    [SerializeField] private BaseSkill pushBox;
    public BaseSkill PushBox { get { return pushBox; } }
    //public BaseSkill jumpSkill2;

    [SerializeField] private BaseSkill wallJump;
    public BaseSkill WallJump { get { return wallJump; } }

    [SerializeField] private BaseSkill atack;
    public BaseSkill Atack { get { return atack; } }

    [Header("UI")]
    public UIMessage uiMessage;
    public GameObject uiGameOver;

    //Functions

    public void SetMessage(string message)
    {
        uiMessage.SetMessage(message);
    }

    public void SetMessage(string message, float duration, float speed)
    {
        uiMessage.SetMessage(message, duration, speed);
    }

    public void EndGame(float time)
    {
        StartCoroutine(WAitAndEndGame(time));
    }

    public void Restart(float time)
    {
        StartCoroutine(WaitAndREstrt(time));
    }

    IEnumerator WaitAndREstrt(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WAitAndEndGame(float time)
    {
        yield return new WaitForSeconds(time);
        uiGameOver.SetActive(true);
        
        yield return new WaitForSeconds(time);
        Application.Quit();
    }



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
