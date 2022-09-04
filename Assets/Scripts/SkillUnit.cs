using UnityEngine;
using System.Collections;

public class SkillUnit : MonoBehaviour, IRecolectable
{
    [SerializeField]
    private BaseSkill skill;

    [SerializeField]
    private float rotateSpedd = 1f;

    [SerializeField]
    private float disolveSpeed = 1f;

    private AudioSource audioOnGet;


    private Renderer currentRenderer;

    public delegate void RecolectableDelegate(BaseSkill skill);
    private RecolectableDelegate onDie;

    public void Recolect(PlayerUnit player)
    {
        if(skill != null)
        {
            player.AddSkill(skill);

            //This set the UI message that the sjull was get
            GameManager.Instance.SetMessage(skill.message);

            skill = null;
        }
        
    }

    public void Die()
    {
        //For Observer pattern, probably is never going to be used
        onDie?.Invoke(skill);

        audioOnGet.Play();

        StartCoroutine(Disolve());
        //it would be nice if some kind of animation is played instead of just destroy it
        //GetComponent<MeshRenderer>().enabled =false;
        //GameObject.Destroy(gameObject);


    }

    private IEnumerator Disolve()
    {
        while (currentRenderer.material.GetFloat("_Disolve") < 1)
        {
            //float currentValue = material.GetFloat("Disolve");
            float newValue = currentRenderer.material.GetFloat("_Disolve") + disolveSpeed * Time.deltaTime;
            currentRenderer.material.SetFloat("_Disolve", newValue);
            yield return null;
        }
        currentRenderer.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        //This maybe would be better if is handle by the player script bur i'm lazy
        PlayerUnit currentPlayer = other.GetComponent<PlayerUnit>();
        if(currentPlayer != null && skill != null)
        {
            Recolect(currentPlayer);
            Die();
        }
    }

    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotateSpedd * Time.deltaTime, transform.eulerAngles.z);
    }

    private void Start()
    {
        //Here add the code to delegate stuffs Maybe
        audioOnGet = GetComponent<AudioSource>();
        currentRenderer = GetComponent<Renderer>();
    }
}
