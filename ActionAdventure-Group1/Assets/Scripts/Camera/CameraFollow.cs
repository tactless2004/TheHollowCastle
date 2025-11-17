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
    [SerializeField] private float minPitch = -20.0f;
    [SerializeField] private float maxPitch = 75.0f;

    [Header("Distance")]
    [SerializeField] private float defaultDistance = 4.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float collisionRadius = 0.3f;
    [SerializeField] private LayerMask collisionMask;

    private Vector3 velocity;
    private float pitch = 20.0f;
    private float yaw = 0.0f;
    private float currentDistance;

    private void Start()
    {
        currentDistance = defaultDistance;

        // Take initial rotations from the camera.
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Orbit();
        CameraCollision();
        Follow();
    }

    private void Orbit()
    {
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
                out RaycastHit hit,
                defaultDistance,
                collisionMask
            ))
        {
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
}
