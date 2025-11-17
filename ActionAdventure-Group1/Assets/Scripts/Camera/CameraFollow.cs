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

    [SerializeField] private float minPitch = 0.0f;
    [SerializeField] private float maxPitch = 75.0f;

    [Header("Distance")]
    [SerializeField] private float distance;

    private Vector3 velocity;
    private float pitch = 20.0f;
    private float yaw = 0.0f;

    private float currentDistance;

    private void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Orbit();
        transform.position = Vector3.SmoothDamp(
            transform.position,
            CalculateDesiredPosition(),
            ref velocity,
            smoothTime
        );
    }

    private void Orbit()
    {
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
}
