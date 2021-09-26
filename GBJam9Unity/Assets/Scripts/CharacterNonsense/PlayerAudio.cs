using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioClip Bawk;
    [SerializeField] float BawkTime;
    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip Throw;
    [SerializeField] AudioClip Roll;
    [SerializeField] AudioClip Died;
    [SerializeField] AudioClip AttackMage;
    [SerializeField] AudioClip SpecialMage;
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

    public void PlayThrow()
    {
        StartCoroutine(PlaySound(Throw, 0));
    }

    public void PlayRoll()
    {
        StartCoroutine(PlaySound(Roll, 0));
    }
    public void PlayDied()
    {
        StartCoroutine(PlaySound(Died, 0));
    }

    public void PlayAttackMage()
    {
        StartCoroutine(PlaySound(AttackMage, 0));
    }
    public void PlaySpecialMage()
    {
        StartCoroutine(PlaySound(SpecialMage, 0));
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
