using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip Bawk;
    [SerializeField] float BawkTime;
    [SerializeField] AudioClip Hit;
    AudioSource ASource;

    private void Awake()
    {
        ASource = GetComponent<AudioSource>();
    }

    public void PlayShout()
    {
        StartCoroutine(PlaySound(Bawk, BawkTime));
    }

    public void PlayHit()
    {
        StartCoroutine(PlaySound(Hit, 0));
    }

    IEnumerator<YieldInstruction> PlaySound(AudioClip clip, float time)
    {

        ASource.PlayOneShot(clip);

        yield return new WaitForSeconds(time);

        if (time != 0)
        {
            ASource.Stop();
        }
    }

}
