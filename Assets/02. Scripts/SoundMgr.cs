using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public AudioClip bgmOST1; //오프닝곡
    public AudioClip bgmOST2; //엔딩곡
    public AudioClip bgmOST3; //포켓몬고
    public AudioClip bgmOST4; //포켓몬골드
    public AudioClip soundBallThrowing;
    public AudioClip coin;

    AudioSource myAudio;
    public static SoundMgr instance;
    void Awake()
    {
        if (SoundMgr.instance == null)
        {
            SoundMgr.instance = this;
        }

        myAudio = this.gameObject.GetComponent<AudioSource>();
    }
    /*void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>();
    }*/

    public void PlaySound(AudioClip whichSound)
    {
        myAudio.PlayOneShot(whichSound);
    }

    public void PlayLoopSound(AudioClip whichSound)
    {
        myAudio.loop = true;
        myAudio.clip = whichSound;
        myAudio.Play();
    }

    void Update()
    {
    }
}
