/************************************************************
* COPYRIGHT:  2025
* PROJECT: the Hollow Castle
* FILE NAME: CameraFollow.cs
* DESCRIPTION: "Soft" follow with right-click orbit + wall collision handling.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/16 | Leyton McKinney | Init
* 2025/11/16 | Leyton McKinney | Add orbit controls.
*
************************************************************/
 
using UnityEngine;
 

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Smoothing")]
    [SerializeField] private float smoothTime = 0.2f;


    [Header("Orbit Settings")]
    [SerializeField] private float orbitSpeed = 150.0f;
    // Pitch snap settings
<<<<<<< HEAD
    [SerializeField] private float minPitch = 0.0f;
    [SerializeField] private float maxPitch = 75.0f;

    [SerializeField] private float distance;
=======
    [SerializeField] private float minPitch = -20.0f;
    [SerializeField] private float maxPitch = 75.0f;

    [Header("Distance")]
    [SerializeField] private float defaultDistance = 4.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float collisionRadius = 0.3f;
    [SerializeField] private LayerMask collisionMask;
>>>>>>> 542d33346e6774b45f259523faeb98a120719775

    private Vector3 velocity;
    private float pitch = 20.0f;
    private float yaw = 0.0f;
<<<<<<< HEAD

    private void Start()
    {
=======
    private float currentDistance;

    private void Start()
    {
        currentDistance = defaultDistance;

        // Take initial rotations from the camera.
>>>>>>> 542d33346e6774b45f259523faeb98a120719775
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

<<<<<<< HEAD
        // Handle orbit input
        Orbit();

        // Calculate desired position with collision
        Vector3 desiredPosition = CalculateDesiredPosition();

        // Smoothly move to desired position
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        // Always look at target
        transform.LookAt(target);
=======
        Orbit();
        CameraCollision();
        Follow();
>>>>>>> 542d33346e6774b45f259523faeb98a120719775
    }

    private void Orbit()
    {
<<<<<<< HEAD
        // Only orbit when right mouse button is held (optional - remove condition if you want always-active orbit)
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch += Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    private Vector3 CalculateDesiredPosition()
    {
        Vector3 direction = (Quaternion.Euler(pitch, yaw, 0.0f) * Vector3.one).normalized;
        return target.position + direction * distance;
    }
    /*
    private Vector3 CalculateDesiredPositionNoClip()
    {
        // Calculate the ideal camera offset
        Vector3 direction = (Quaternion.Euler(pitch, yaw, 0) * Vector3.back).normalized;

        // Check for collisions
        if (Physics.SphereCast(
                target.position,
                collisionRadius,
                direction,
=======
        yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
        // maybe reverse these axes?
        pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void CameraCollision()
    {
        Vector3 desiredOffset = Quaternion.Euler(pitch, yaw, 0) * new Vector3(0, 0, -defaultDistance);
        Vector3 desiredPosition = target.position + desiredOffset;

        if (Physics.SphereCast(
                target.position,
                collisionRadius,
                (desiredPosition - target.position).normalized,
>>>>>>> 542d33346e6774b45f259523faeb98a120719775
                out RaycastHit hit,
                defaultDistance,
                collisionMask
            ))
        {
<<<<<<< HEAD
            // Hit something - pull camera closer
            currentDistance = Mathf.Clamp(hit.distance - 0.1f, minDistance, defaultDistance);
        }
        else
        {
            // No obstruction - smoothly return to default distance
            currentDistance = Mathf.Lerp(currentDistance, defaultDistance, Time.deltaTime * 5.0f);
        }

        // Apply the (possibly adjusted) distance
        return target.position + direction * currentDistance;
    }
    */
=======
            currentDistance = Mathf.Clamp(hit.distance - 0.1f, minDistance, default);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, defaultDistance, Time.deltaTime * 5.0f);
        }

        Vector3 adjustedOffset = Quaternion.Euler(pitch, yaw, 0.0f) * new Vector3(0, 0, -currentDistance);

        transform.position = target.position + adjustedOffset;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0.0f);
    }

    private void Follow()
    {
        // remove offset
        Vector3 targetPosition = target.position;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
>>>>>>> 542d33346e6774b45f259523faeb98a120719775
}
