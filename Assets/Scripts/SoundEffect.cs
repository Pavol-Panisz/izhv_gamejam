using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public SoundPlayType playType = SoundPlayType.random;
    [Tooltip("Only allow playing if the previous sound finished playing?")]
    public bool playOnlyIfFinished = true;

    public AudioClip[] sounds;

    private int lastPlayedSoundIndex = 0;


    public enum SoundPlayType { sequential, random }

    public void Awake()
    {
        // we think that the last sound in the sounds list was played.
        // thererofre, when you play a sound for the first time, it starts with the first
        // if the playType is sequential
        lastPlayedSoundIndex = sounds.Length - 1;
    }

    public void PlaySound()
    {
        if (sounds.Length == 0) {
            Debug.LogError("No sounds assigned to SoundEffect on " + transform.name);
            return;
        }

        if (playOnlyIfFinished && audioSource.isPlaying) { return; }

        if (sounds.Length == 1)
        {
            audioSource.clip = sounds[0];
            audioSource.Play();
            return;
        }

        if (playType == SoundPlayType.random)
        {
            int randNum = Random.Range(0, sounds.Length);

            do // this thing plays a random sound but never the same one twice in a row
            {
                if (lastPlayedSoundIndex <= randNum) { randNum += 1; }
                randNum %= sounds.Length;
            }
            while (randNum == lastPlayedSoundIndex);

            audioSource.clip = sounds[randNum];
            lastPlayedSoundIndex = randNum;
            audioSource.Play();
        } 
        else // play sequentially 
        {
            lastPlayedSoundIndex++;
            lastPlayedSoundIndex %= sounds.Length;
            audioSource.clip = sounds[lastPlayedSoundIndex];
            audioSource.Play();
        }
    }
}
