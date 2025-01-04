using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHeleco : MonoBehaviour
{
  public Transform airplane;  // Reference to the airplane
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // Offset to position the camera behind the airplane
    public float positionSmoothSpeed = 0.1f;   // Smoothing factor for the camera position
    public float rotationSmoothSpeed = 0.1f;   // Smoothing factor for the camera rotation
    public float zoomSpeed = 5f;               // Zoom speed for the camera
    public float minZoom = 5f;                 // Minimum zoom distance
    public float maxZoom = 20f;                // Maximum zoom distance

    private float currentZoomLevel;            // Current zoom level (distance from the airplane)
    private Camera mainCamera;                 // Reference to the main camera

    void Start()
    {
        mainCamera = Camera.main;  // Get the main camera reference
        currentZoomLevel = Vector3.Distance(transform.position, airplane.position); // Initialize zoom distance
    }

    void Update()
    {
        HandleZoom();  // Handle zooming in/out with the mouse scroll
    }

    void LateUpdate()
    {
        // Calculate the desired position based on airplane's position and the offset
        Vector3 desiredPosition = airplane.position + airplane.rotation * cameraOffset;

        // Smoothly interpolate the camera's position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed);

        // Smoothly interpolate the camera's rotation to always face the airplane
        Quaternion desiredRotation = Quaternion.LookRotation(airplane.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed);
    }

    void HandleZoom()
    {
        // Zoom in/out with the mouse scroll wheel
        currentZoomLevel -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoomLevel = Mathf.Clamp(currentZoomLevel, minZoom, maxZoom);  // Clamp zoom within specified range

        // Adjust camera position based on the zoom level
        cameraOffset.z = -currentZoomLevel;
    }
}
