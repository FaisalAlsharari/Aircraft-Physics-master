using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class HelicopterFollowCam : MonoBehaviour
{
    public Transform helicopter;        // Reference to the helicopter
    public Vector3 offset = new Vector3(0, 5, -10); // Camera's offset position from the helicopter
    public float smoothSpeed = 0.125f; // Smoothing factor for position
    public float rotationSmoothSpeed = 0.1f; // Smoothing factor for rotation
    public float zoomSpeed = 5f; // Speed at which the camera zooms in/out
    public float minZoom = 5f;  // Minimum zoom distance
    public float maxZoom = 15f; // Maximum zoom distance

    private Camera cam;          // Reference to the camera
    private float currentZoom;   // Current zoom level

    void Start()
    {
        cam = Camera.main; // Get the main camera reference
        currentZoom = Vector3.Distance(transform.position, helicopter.position); // Initialize zoom distance
    }

    void Update()
    {
        // Adjust camera zoom with mouse scroll
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Apply zoom to the offset (Z-axis zoom)
        offset.z = -currentZoom;

        // Call method to follow the helicopter smoothly
        FollowHelicopter();
    }

    void FollowHelicopter()
    {
        // Smoothly interpolate position
        Vector3 desiredPosition = helicopter.position + helicopter.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Smoothly interpolate rotation
        Quaternion desiredRotation = Quaternion.LookRotation(helicopter.position - transform.position);
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed);
        transform.rotation = smoothedRotation;
    }
}
