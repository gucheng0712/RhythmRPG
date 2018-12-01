using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_Audio : GameManager<GameManager_Audio>
    {
        public AudioSource fxSrc;
        public AudioSource musicSrc;

        public AudioClip btnClip;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            fxSrc.PlayOneShot(clip);
            print("playcLIP");
        }


        // Used to play Btn Sound
        public void PlayBtnSound()
        {
            fxSrc.PlayOneShot(btnClip);
        }


        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void RandomizeFX(AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);

            float randomPitch = Random.Range(.95f, 1.05f);

            fxSrc.pitch = randomPitch;

            fxSrc.clip = clips[randomIndex];

            fxSrc.Play();
        }
    }
}