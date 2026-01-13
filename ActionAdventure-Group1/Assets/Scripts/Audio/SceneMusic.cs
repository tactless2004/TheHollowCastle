/************************************************************
* COPYRIGHT:  2026
* PROJECT: The Hollow Castle
* FILE NAME: SceneMusic.cs
* DESCRIPTION: Plays music on a per-scene basis.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/13 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;


public class SceneMusic : MonoBehaviour
{
    [SerializeField] private MusicTrack track;

    private void Start()
    {
        var gm = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<MusicDirector>();
        gm?.Play(track);
    }
}
