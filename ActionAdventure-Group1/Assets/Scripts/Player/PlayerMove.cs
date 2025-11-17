/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: PlayerMove.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/16 | Leyton McKinney | Use camera orientation to orient player movement.
*
************************************************************/

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System; // Need for Enum
 

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    
    private enum SnapType
    {
        FourDirection,
        EightDirection
    }

    [SerializeField] private Transform cameraTransform;

    [Header("SNAP PARAMS")]
    [SerializeField]
    private SnapType snapType = SnapType.FourDirection;

    [SerializeField, Tooltip("If the isogrid is rotated, this will fix the snap")]
    private float gridRotationOffsetY = 0.0f;
    private Quaternion gridRotationOffset;

    [SerializeField, Tooltip("Direction the player is facing. \nREADONLY DON'T MODIFY")]
    public Vector3 facing;

    [SerializeField, Tooltip("If true, smoothly rotates to snap orientation instead of snapping.")]
    private bool smoothSnap = false;

    [SerializeField, Tooltip("Rotation speed for snapping when smoothSnap enable")]
    private float rotationSpeed = 10f;

    private Vector3 cachedDirection;

    [Header("MOVEMENT PARAMS")]
    [SerializeField]
    private float MAX_FORCE = 10.0f;

    [SerializeField]
    private float _forceApplied;

    [SerializeField]
    private Vector3 _direction;
    
    public float ForceApplied
    {
        get => _forceApplied;
        set => _forceApplied = Mathf.Clamp(value, 0.0f, MAX_FORCE);
    }

    public Vector3 Direction
    {
        get => _direction;
        set {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0.0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0.0f;
            camRight.Normalize();

            _direction = camForward * value.z + camRight * value.x;
        }
    }
    // Awake is called once on initialization (before Start)
    private void Awake()
    {
        facing = Vector3.forward;
        cachedDirection = Vector3.zero;
        gridRotationOffset = Quaternion.Euler(0.0f, gridRotationOffsetY, 0.0f);
        if(!TryGetComponent<Rigidbody>(out rb))
        {
            Debug.LogError("Rigidbody not found on Player!");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = Direction * ForceApplied;

        // This creates some semblance of momentum, so the player can't change direction on a dime
        // but, movement also doesn't feeling slippery. A good balance methinks.
        Vector3 dv = new Vector3(
            targetVelocity.x - currentVelocity.x,
            0.0f,
            targetVelocity.z - currentVelocity.z
        );
        rb.AddForce(dv, ForceMode.VelocityChange);

        // Snap to direction
        SnapDirection();
    }

    private void SnapDirection()
    {
        Vector3 intendedDirection = Direction != Vector3.zero ? Direction : cachedDirection;

        // If cachedDirection was Vector3.zero, just bail
        if (intendedDirection == Vector3.zero) return;

        Vector3 snappedDir = intendedDirection;

        if (snapType == SnapType.FourDirection)
        {
            // Whichever Axis has a higher influence on the direction, we snap that way
            if (Mathf.Abs(intendedDirection.x) > Mathf.Abs(intendedDirection.z))
            {
                // Snap x
                snappedDir = new Vector3(Mathf.Sign(intendedDirection.x), 0.0f, 0.0f);
            }
            else
            {
                // Snap z
                snappedDir = new Vector3(0.0f, 0.0f, Mathf.Sign(intendedDirection.z));
            }
        } 
        
        if (!Enum.IsDefined(typeof(SnapType), snapType))
        {
            Debug.LogError("No SnapType Chosen!\nDefaulting to Eight Direction.");
        }

        Vector3 worldSnappedDir = gridRotationOffset * snappedDir;

        Quaternion snapTarget = Quaternion.LookRotation(worldSnappedDir);

        // Apply Snap
        if (smoothSnap)
        {
            rb.MoveRotation(
                Quaternion.Slerp(
                    transform.rotation,
                    snapTarget,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }

        else
        {
            rb.MoveRotation(snapTarget);
        }

        cachedDirection = snappedDir;
        facing = worldSnappedDir;
    }
}
