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

    [SerializeField]
    private SnapType snapType = SnapType.FourDirection;

    [SerializeField]
    [Tooltip("If the isogrid is rotated, this will fix the snap")]
    private float gridRotationOffsetY = 0.0f;

    [SerializeField]
    [Tooltip("Direction the player is facing. \nREADONLY DON'T MODIFY")]
    public Vector3 facing;

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
        set => _direction = value.normalized;
    }
    // Awake is called once on initialization (before Start)
    private void Awake()
    {
        facing = Vector3.forward;
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
        if(Direction != Vector3.zero) SnapDirection();
    }

    private void SnapDirection()
    {
        Vector3 snappedDir = Direction;
        if (snapType == SnapType.FourDirection)
        {
            // Whichever Axis has a higher influence on the direction, we snap that way
            if (Mathf.Abs(Direction.x) > Mathf.Abs(Direction.z))
            {
                // Snap x
                snappedDir = new Vector3(Mathf.Sign(Direction.x), 0.0f, 0.0f);
            }
            else
            {
                // Snap z
                snappedDir = new Vector3(0.0f, 0.0f, Mathf.Sign(Direction.z));
            }
        } 
        
        if (!Enum.IsDefined(typeof(SnapType), snapType))
        {
            Debug.LogError("No SnapType Chosen!\nDefaulting to Eight Direction.");
        }

        // fix local snappedDir to worldSnappedDir
        Quaternion gridRotationOffset = Quaternion.Euler(0.0f, gridRotationOffsetY, 0.0f);
        Vector3 worldSnappedDir = gridRotationOffset * snappedDir;
        // Apply Snap
        transform.rotation = Quaternion.LookRotation(worldSnappedDir);
        facing = worldSnappedDir;
    }
}
