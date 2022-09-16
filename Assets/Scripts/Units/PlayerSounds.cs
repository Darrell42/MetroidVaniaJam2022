using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    private AudioSource audioSorce;

    [SerializeField]
    private List<AudioClip> jumpAudio;
    [SerializeField]
    private List<AudioClip> stepsAudio;
    [SerializeField]
    private List<AudioClip> dounlrJumpAudio;
    [SerializeField]
    private List<AudioClip> pushBox;
    [SerializeField]
    private List<AudioClip> Slide;
    [SerializeField]
    private List<AudioClip> Land;





    public void PlayAudioJump()
    {
        PlayAudio(jumpAudio, 1f);
    }

    public void PlayAudioDoubleJump()
    {
        PlayAudio(dounlrJumpAudio, 1f);
    }

    public void PlayStepAudio()
    {
        PlayAudio(stepsAudio, 0.3f);
    }

    public void PlayAudioSlide()
    {
        PlayAudio(Slide, 0.5f);
    }

    public void PlayAudioLand()
    {
        PlayAudio(Land, 1f);
    }

    public void PlayPoshBox()
    {
        PlayAudioLoop(pushBox, true);
    }


    public void StopAudioLoop()
    {
        audioSorce.Stop();
        audioSorce.loop = false;
        audioSorce.clip = null;
    }



    public void PlayAudio(List<AudioClip> audio, float volumeScale)
    {
        if(jumpAudio != null)
        {

            if (audio.Count <= 0) return;

            int randomAudio = Random.Range(0, audio.Count - 1);


            //audioSorce.loop = loop;
            //audioSorce.clip = audio[randomAudio];
            //audioSorce.Play();
            audioSorce.PlayOneShot(audio[randomAudio], volumeScale);
        }
    }

    public void PlayAudioLoop(List<AudioClip> audio, bool loop)
    {
        if (jumpAudio != null)
        {

            if (audio.Count <= 0) return;

            int randomAudio = Random.Range(0, audio.Count - 1);


            audioSorce.loop = loop;
            audioSorce.clip = audio[randomAudio];
            audioSorce.Play();
            //audioSorce.PlayOneShot(audio[randomAudio], volumeScale);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
