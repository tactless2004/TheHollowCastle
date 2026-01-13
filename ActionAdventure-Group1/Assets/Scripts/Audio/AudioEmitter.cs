/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: AudioEmitter.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
*
*
************************************************************/
 
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioEmitter : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        // We don't want this to fire off rip
        source.playOnAwake = false;
    }

    public void Play(SoundEvent sound)
    {
        // Don't play sounds that don't exist lol
        if (sound == null) return;

        AudioClip clip = sound.GetRandomClip();
        if (clip == null) return;

        // Actually play the sound now
        source.clip = clip;
        source.volume = Random.Range(sound.volumeMin, sound.volumeMax);
        source.pitch = Random.Range(sound.pitchMin, sound.pitchMax);
        source.spatialBlend = sound.spatial ? 1.0f : 0.0f;
        source.minDistance = sound.minDistance;
        source.maxDistance = sound.maxDistance;
        if (source.outputAudioMixerGroup != null)
            source.outputAudioMixerGroup = sound.mixerGroup;

        source.Play();
    }
    public void PlayOneShot(SoundEvent sound)
    {
        // Don't play sounds that don't exist lol
        if (sound == null) return;

        AudioClip clip = sound.GetRandomClip();
        if (clip == null) return;

        source.pitch = Random.Range(sound.pitchMin, sound.pitchMax);
        source.spatialBlend = sound.spatial ? 1.0f : 0.0f;
        if (source.outputAudioMixerGroup != null)
            source.outputAudioMixerGroup = sound.mixerGroup;

        source.PlayOneShot(
            clip,
            Random.Range(sound.volumeMin, sound.volumeMax)
        );
    }
}
