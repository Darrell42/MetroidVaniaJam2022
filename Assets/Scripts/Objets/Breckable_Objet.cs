using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breckable_Objet : MonoBehaviour,IDamagable, IBrekable
{
    [Header("Atributes")]
    [SerializeField]
    private float resistance = 100f;

    [SerializeField]
    private float explosionAdditive = 0f;

    [SerializeField]
    private GameObject piecesPArent;

    [SerializeField]
    private float disoveSped = 1f;

    [SerializeField]
    private float waitForSeconds = 2f;

    public bool canResetPosition;
    public bool canDie;

    [Header("VFX")]
    [SerializeField]
    private ParticleSystem explosionParticle;

    [SerializeField]
    private ParticleSystem cutParticle;

    [Header("Audio")]
    [SerializeField]
    private AudioClip breackAudio;

    private AudioSource audioSource;

    [Header ("Do not Touch")]
    [SerializeField]
    private GameObject orignalPeace;

    [SerializeField]
    private Vector3 orignalPeacePosition;

    [SerializeField]
    private Quaternion originalPeaceRotation;
    
    [SerializeField]
    private List<GameObject> peaces;

    [SerializeField]
    private List<Vector3> peacesPosition;

    [SerializeField]
    private List<Quaternion> peacesRotation;

    public int count;

    private Material material;


    public void ApplyDamage(float damage)
    {
        Breack(damage);

        if(explosionParticle != null)
        {
            explosionParticle.Emit(1);
        }

        if (audioSource != null) audioSource.Play();

        if (canResetPosition && !canDie)
        {
            StartCoroutine(Reset(disoveSped));
            //RessePosition();
        }

        if (canDie && !canResetPosition)
        {
            StartCoroutine(Die(disoveSped));
        }
    }

    public void ApplyDamage(float damage, Vector3 direction)
    {
        Breack(damage, direction);

        if (explosionParticle != null)
        {
            cutParticle.Emit(1);
        }

        if(audioSource != null) audioSource.Play();

        if (canResetPosition && !canDie)
        {
            StartCoroutine(Reset(disoveSped));
            //RessePosition();
        }

        if (canDie && !canResetPosition)
        {
            StartCoroutine(Die(disoveSped));
        }
    }

    public void DesactivateOrigin()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        
    }
    public void ActivateOrigin()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;

    }

    public IEnumerator Reset(float speed)
    {
        yield return new WaitForSeconds(waitForSeconds);

        while(material.GetFloat("_Disolve") < 1)
        {
            float newValuw = material.GetFloat("_Disolve") + speed * Time.deltaTime;
            material.SetFloat("_Disolve", newValuw);
            yield return null;
        }
        RessePosition();

        while (material.GetFloat("_Disolve") > -0.1)
        {
            float newValuw = material.GetFloat("_Disolve") - speed * Time.deltaTime;
            material.SetFloat("_Disolve", newValuw);
            yield return null;
        }

    }
    public IEnumerator Die(float speed)
    {
        yield return new WaitForSeconds(waitForSeconds);

        while (material.GetFloat("_Disolve") < 1)
        {
            float newValuw = material.GetFloat("_Disolve") + speed * Time.deltaTime;
            material.SetFloat("_Disolve", newValuw);
            yield return null;
        }
        piecesPArent.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void RessePosition()
    {
        transform.localPosition = orignalPeacePosition;
        transform.localRotation = originalPeaceRotation;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        piecesPArent.transform.parent = transform;
        piecesPArent.transform.localPosition = Vector3.zero;

        if (piecesPArent != null && peacesPosition  != null && peacesRotation !=null)
        {
            for (int i = 1; i < piecesPArent.transform.childCount; i++)
            {
                piecesPArent.transform.GetChild(i).localPosition = peacesPosition[i -1];
                piecesPArent.transform.GetChild(i).localRotation = peacesRotation[i -1];

                piecesPArent.transform.GetChild(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
                piecesPArent.transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                piecesPArent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        //
        //orignalPeace.SetActive(true);
        ActivateOrigin();

    }

    public void Breack(float damage)
    {
        if(orignalPeace != null && peaces != null)
        {

            piecesPArent.transform.parent = null;
            piecesPArent.transform.position = orignalPeace.transform.position;
            //piecesPArent.transform.localScale = orignalPeace.transform.localScale;

            //int random = Random.Range(0, 1);

            float minForce = 0f;
            float maxForce = 15;
            foreach (GameObject x in peaces)
            {
                x.SetActive(true);

                float forceToapplt = Random.Range(minForce, maxForce);

                //if(random == 1)
                //x.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceToapplt, ForceMode.Impulse);
                x.GetComponent<Rigidbody>().AddExplosionForce(damage, orignalPeace.transform.position, damage);
            }

            //orignalPeace.SetActive(false);
            DesactivateOrigin();
        }
    }

    public void Breack(float damage, Vector3 direction)
    {
        if (orignalPeace != null && peaces != null)
        {

            piecesPArent.transform.parent = null;
            piecesPArent.transform.position = orignalPeace.transform.position;
            //piecesPArent.transform.localScale = orignalPeace.transform.localScale;

            int random = Random.Range(0, 1);

            float minForce = 0f;
            float maxForce = damage;
            foreach (GameObject x in peaces)
            {
                x.SetActive(true);

                float forceToapplt = Random.Range(minForce, maxForce);

                //if(random == 1)
                x.GetComponent<Rigidbody>().AddForce(direction * forceToapplt, ForceMode.Impulse);
                //x.GetComponent<Rigidbody>().AddExplosionForce(50f, orignalPeace.transform.position, 50f);
            }

            //orignalPeace.SetActive(false);
            DesactivateOrigin();
        }

    }

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();

       count = piecesPArent.transform.childCount;

        orignalPeace = this.gameObject;
        orignalPeacePosition = transform.localPosition;
        originalPeaceRotation = transform.localRotation;

        peaces = new List<GameObject>();
        peacesPosition = new List<Vector3>();
        peacesRotation = new List<Quaternion>();

        if (piecesPArent != null)
        {
            for (int i = 1; i < piecesPArent.transform.childCount; i++)
            {
                peaces.Add(piecesPArent.transform.GetChild(i).gameObject);
                piecesPArent.transform.GetChild(i).GetComponent<Renderer>().material = material;

                peacesPosition.Add(piecesPArent.transform.GetChild(i).transform.localPosition);
                peacesRotation.Add(piecesPArent.transform.GetChild(i).transform.localRotation);

            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > resistance)
        {
            ApplyDamage(collision.relativeVelocity.magnitude + explosionAdditive);
        }
    }



}
