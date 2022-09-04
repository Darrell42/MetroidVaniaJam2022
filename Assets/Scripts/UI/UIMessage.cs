using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class UIMessage : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textMessage;

    [SerializeField]
    private RectTransform panel;

    [SerializeField]
    private float messageDuration = 1f;

    [SerializeField]
    private float messageSpeed = 0.5f;

    private CanvasGroup canvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        GameManager.Instance.uiMessage = this;

    }

    public void SetMessage(string message, float duration, float fadeSpeed)
    {
        textMessage.text = message;

        StartCoroutine(Apear(duration, fadeSpeed));
    }

    public void SetMessage(string message)
    {
        textMessage.text = message;

        StartCoroutine(Apear(messageDuration, messageSpeed));
    }

    #region Coorutine

    private IEnumerator Apear(float duration, float fadeSpeed)
    {
        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(waitForSecod(duration, fadeSpeed));
    }

    private IEnumerator waitForSecod(float second, float speed)
    {
        yield return new WaitForSeconds(second);
        StartCoroutine(Fade(second, speed));
    }

    private IEnumerator Fade(float duration, float speed)
    {
         while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= speed * Time.deltaTime;
            yield return null;
        }
    }

    #endregion


}
