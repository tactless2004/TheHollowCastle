/************************************************************
* COPYRIGHT:  2026
* PROJECT: The Hollow Castle
* FILE NAME: PlayerPickup.cs
* DESCRIPTION: Handles Raycasts to see if the Player is looking at a "Pickupable" item.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/12 | Leyton McKinney | Init
*
************************************************************/

using System;
using UnityEngine;
 

public class PlayerPickup : MonoBehaviour
{
    [SerializeField, Tooltip("Maximum distance ahead of the player where items can be picked up")]
    private float pickupRange = 3.0f;

    [SerializeField, Tooltip("Only items on this layer can be picked up")]
    private LayerMask pickupMask;

    [SerializeField, Tooltip("How often (in ms) should the check ray be cast")]
    private float checkInterval;

    // Current pickup keeps track of the item last seen, can be null
    // In general scripts should subscribe to the OnPickupSeen and OnPickupLost events
    // instead of directly polling this variable.
    public PickupItem CurrentPickup { get; private set; }

    private float timer;

    // Handles how many consecutive misses can occur before we throw out the 
    // current PickupItem.
    private int missCount;
    private const int MISSTHRESHOLD = 2;

    public event Action<PickupItem> OnPickupSeen;
    public event Action OnPickupLost;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = checkInterval;
            CheckForPickup();
        }
    }

    void CheckForPickup()
    {
        // The look ray is shifted up, because the ray is at the player's feet by default
        Ray ray = new Ray(transform.position + 2 * Vector3.up, transform.forward);

        // Player is looking at a pickupable item
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupMask))
        {
            PickupItem pickup = hit.collider.GetComponent<PickupItem>();
            missCount = 0;

            if (pickup != CurrentPickup)
            {
                CurrentPickup = pickup;
                OnPickupSeen?.Invoke(pickup);
            }
        }

        // Player is not looking at a pickupable item
        else
        {
            HandleMiss();
        }
    }

    void HandleMiss()
    {
        if (CurrentPickup == null) return;

        missCount++;
        if (missCount >= MISSTHRESHOLD)
        {
            CurrentPickup = null;
            missCount = 0;
            OnPickupLost?.Invoke();
        }
    }
}
