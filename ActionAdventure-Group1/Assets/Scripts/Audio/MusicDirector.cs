/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: MusicDirector.cs
* DESCRIPTION: Manages crossfading between songs during GamePlay.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/13 | Leyton McKinney | Init
*
************************************************************/

using UnityEngine;
using System.Collections;


public class MusicDirector : MonoBehaviour
{
    [SerializeField] private AudioSource a;
    [SerializeField] private AudioSource b;

    // used for cross fading
    private AudioSource current;
    private AudioSource next;

    private void Awake()
    {
        if (!a)
        {
            a = gameObject.AddComponent<AudioSource>();
        }

        if (!b)
        {
            b = gameObject.AddComponent<AudioSource>();
        }

        a.playOnAwake = false;
        b.playOnAwake = false;

        current = a;
        next = b;

        current.volume = 0.0f;
        next.volume = 0.0f;
    }

    public void Play(MusicTrack track, float fadeSeconds = 1.0f)
    {
        if (track == null || track.clip == null) return;

        // If the requested MusicTrack is already playing, don't need to restart it.
        if (current.isPlaying && current.clip == track.clip) return;

        StopAllCoroutines();
        StartCoroutine(CrossfadeTo(track, fadeSeconds));
    }

    private IEnumerator CrossfadeTo(MusicTrack track, float fadeSeconds)
    {
        next.clip = track.clip;
        next.loop = track.loop;
        next.volume = 0.0f;
        next.Play();

        float t = 0.0f;
        float startCurrent = current.volume;
        float targetNext = track.volume;

        while (t < fadeSeconds)
        {
            t += Time.unscaledDeltaTime;
            float k = fadeSeconds <= 0.0f ? 1.0f : Mathf.Clamp01(t / fadeSeconds);

            current.volume = Mathf.Lerp(startCurrent, 0.0f, k);
            next.volume = Mathf.Lerp(0.0f, targetNext, k);

            yield return null;
        }

        current.Stop();
        current.volume = 0.0f;

        // swap
        var tmp = current;
        current = next;
        next = tmp;
    }
}
