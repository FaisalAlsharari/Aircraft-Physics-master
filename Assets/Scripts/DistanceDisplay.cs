using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DistanceDisplay : MonoBehaviour
{
    public GameObject helicopter; // Reference to the helicopter
    public List<GameObject> enemies = new List<GameObject>();  // List of enemies
    public Text distanceText; // UI Text to show the distance

    void Update()
    {
        // Check if there are enemies in the list
        if (enemies.Count > 0)
        {
            // Find the nearest enemy
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(helicopter.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            // Update the UI with the distance to the closest enemy
            distanceText.text = "Closest Enemy Distance: " + closestDistance.ToString("F2") + " meters";
        }
        else
        {
            distanceText.text = "No Enemies Detected"; // If no enemies are in the list
        }
    }
}
