using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointSystem : MonoBehaviour
{
    // List of all waypoints (targets)
    public List<Transform> checkPoints;
    public Text distanceText;   // Text UI element to display distance information

    private void Update()
    {
        // If there are no waypoints in the list, return early
        if (checkPoints.Count == 0) return;

        string displayText = "";    // String to accumulate the distance information for each checkpoint

        // Loop through each waypoint to calculate the distance
        foreach (Transform checkpoint in checkPoints)
        {
            // Calculate the distance between the helicopter and the current checkpoint
            float distance = (checkpoint.position - transform.position).magnitude;

            // Append the distance information to the displayText
            displayText += checkpoint.name + ": " + distance.ToString("F1") + " meters\n";
        }

        // Update the distance UI text to show the information
        distanceText.text = displayText;
    }
}
