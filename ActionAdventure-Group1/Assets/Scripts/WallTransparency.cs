/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: WallTransparency.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/17 | Peyton Lenard | Created class
* 2025/11/18 | Peyton Lenard | Fixed Trigger methods and transform position logic
*
************************************************************/
 
using UnityEngine;
using System.Collections.Generic; // HashSet

public class WallTransparency : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camTransform;
    private HashSet<Renderer> hiddenRenderers = new HashSet<Renderer>();
    private Vector3 raycastOffset = new Vector3(0.0f, 1.25f, 0.0f);

    private void Awake()
    {
        ReacquireCamera();
        ReacquirePlayer();        
    }

    private void Update()
    {
        // 1.) Make all invisible walls visible
        foreach (Renderer r in hiddenRenderers)
        {
            if (r != null) r.enabled = true;
        }
        hiddenRenderers.Clear();

        // 2.) If player or camera are null, attempt to reacquire, 
        // if reaquisition fails exit.
        if (player == null) ReacquirePlayer();
        if (player == null) return;
        if (camTransform == null) ReacquireCamera();
        if (camTransform == null) return;

        // 3.) Raycast from camera to player.
        RaycastHit[] hits = Physics.RaycastAll(
            camTransform.position,
            (player.position + raycastOffset - camTransform.position).normalized,
            1000.0f
        );

        // 3a.) If nothing is hit, bail
        if(hits.Length == 0) return;

        // 4.) Sort raycast hits in ascending order of distance, because they're not guarenteed to be
        // "in order".
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        // 5.) Disable the renderer of every object between the player and camera
        foreach(RaycastHit hit in hits)
        {
            // 5a.) MainCamera and RaycastIgnore tags should not be disabled
            if (
                hit.collider.CompareTag("MainCamera") ||
                hit.collider.CompareTag("RaycastIgnore") ||
                hit.collider.CompareTag("Enemy") ||
                hit.collider.CompareTag("Doors")
            ) continue;

            // 5b.) Once the player is hit (because we're iterating in descending order of distance from the camera),
            // then all the obstructions are disabled
            if (hit.collider.CompareTag("Player")) break;
            
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r != null)
            {
                r.enabled = false;
                hiddenRenderers.Add(r);
            }
        }
    }

    private void ReacquirePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void ReacquireCamera()
    {
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
}//end WallTransparency
