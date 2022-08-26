using UnityEngine;

public class SkillUnit : MonoBehaviour, IRecolectable
{
    [SerializeField]
    private BaseSkill skill;

    [SerializeField]
    private float rotateSpedd = 1f;

    private AudioSource audioOnGet;

    public delegate void RecolectableDelegate(BaseSkill skill);
    private RecolectableDelegate onDie;

    public void Recolect(PlayerUnit player)
    {
        if(skill != null)
        {
            player.AddSkill(skill);
            skill = null;
        }
        
    }

    public void Die()
    {
        //For Observer pattern, probably is never going to be used
        onDie?.Invoke(skill);

        audioOnGet.Play();

        //it would be nice if some kind of animation is played instead of just destroy it
        GetComponent<MeshRenderer>().enabled =false;
        //GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //This maybe would be better if is handle by the player script bur i'm lazy
        PlayerUnit currentPlayer = other.GetComponent<PlayerUnit>();
        if(currentPlayer != null)
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
    }
}
