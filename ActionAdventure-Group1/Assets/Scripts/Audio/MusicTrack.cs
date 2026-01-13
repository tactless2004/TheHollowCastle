/************************************************************
* COPYRIGHT:  2026
* PROJECT: the Hollow Castle
* FILE NAME: MusicTrack.cs
* DESCRIPTION: Defines a MusicTrack, which plays a song, this is distinct from SoundEvent for sound effects.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/13 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music Track")]
public class MusicTrack : ScriptableObject
{
    public AudioClip clip;
    [Range(0.0f, 1.0f)] public float volume = 1.0f;
    public bool loop = true;
}
