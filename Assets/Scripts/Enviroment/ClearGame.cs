using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGame : MonoBehaviour
{
    [SerializeField]
    private Door door;

    private bool endGameCondition;


    public void EndGameConditionMet()
    {
        endGameCondition = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        door.onOpen += EndGameConditionMet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerUnit player = other.GetComponent<PlayerUnit>();
        
        if(player != null && endGameCondition)
        {
            Debug.Log("GAmeOver");
            GameManager.Instance.cameraFallow.transform.parent = null;
            GameManager.Instance.Player.controls.Disable();
            GameManager.Instance.Player.moveInput = new Vector2(1f, 0);

            GameManager.Instance.Player.GetComponent<AudioSource>().volume = 0;

            GameManager.Instance.EndGame(3f);
        }

    }

}
