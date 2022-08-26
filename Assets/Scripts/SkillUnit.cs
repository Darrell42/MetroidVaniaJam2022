using UnityEngine;

public class SkillUnit : MonoBehaviour, IRecolectable
{
    [SerializeField]
    private BaseSkill skill;

    [SerializeField]
    private float rotateSpedd = 1f;

    [SerializeField]
    private AudioClip audioOnGet;

    public delegate void RecolectableDelegate(BaseSkill skill, AudioClip audio);
    private RecolectableDelegate onDie;

    public void Recolect(PlayerUnit player)
    {
        if(skill != null)
        {
            player.AddSkill(skill);
        }
        
    }

    public void Die()
    {
        onDie?.Invoke(skill, audioOnGet);

        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
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
    }
}
