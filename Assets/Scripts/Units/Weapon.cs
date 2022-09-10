using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    PlayerCombat combat;

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> swingAudios;

    [SerializeField]
    private float minPitch = 0.75f;

    [SerializeField]
    private float maxPitch = 1.3f;


    // Start is called before the first frame update
    void Start()
    {
        combat = GameManager.Instance.Player.GetComponent<PlayerCombat>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwingAudio()
    {
        if(swingAudios.Count > 1)
        {
            int randomAudio = Random.Range(0, swingAudios.Count);
            float randomPitch = Random.Range(minPitch, maxPitch);
            float previousPitch = audioSource.pitch;

            audioSource.clip = swingAudios[randomAudio];
            audioSource.pitch = randomPitch;
            audioSource.Play();
            
            //audioSource.pitch = previousPitch;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();

        if(damagable != null)
        {
            bool damagableAlreadyInList = combat.damagables.Find(x => x.Equals(damagable)) != null;

            if (!damagableAlreadyInList)
            {
                //damagable.ApplyDamage(combat.power);
                damagable.ApplyDamage(combat.power,transform.right);
                combat.damagables.Add(damagable);

            }
                
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.collider.GetComponent<IDamagable>();

        if (damagable != null)
        {
            bool damagableAlreadyInList = combat.damagables.Find(x => x.Equals(damagable)) != null;

            if (!damagableAlreadyInList)
            {
                damagable.ApplyDamage(combat.power + 100f);
                combat.damagables.Add(damagable);

            }

        }

    }
}
