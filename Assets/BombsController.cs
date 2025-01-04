using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsController : MonoBehaviour
{
    public GameObject exp;  // Explosion prefab
    public float expForce, radius;  // Explosion force and radius

    private List<Bomb> bombs = new List<Bomb>();  // List to store references to all bombs

    void Start()
    {
        // Find all Bomb scripts in the children and add them to the bombs list
        foreach (Transform child in transform)
        {
            Bomb bomb = child.GetComponent<Bomb>();
            if (bomb != null)
            {
                bombs.Add(bomb);
            }
        }
    }

    void Update()
{
    // Ensure bombs are only triggered when helicopter is not on the ground
    if (!GetComponent<HelicopterController>().isOnGround) 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TriggerBombFall(0);  // Fall the first bomb
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TriggerBombFall(1);  // Fall the second bomb
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TriggerBombFall(2);  // Fall the third bomb
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TriggerBombFall(3);  // Fall the fourth bomb
        }
    }
}


    void TriggerBombFall(int index)
    {
        if (index >= 0 && index < bombs.Count)
        {
            Bomb bomb = bombs[index];  // Get the bomb at the specified index
            if (bomb != null && !bomb.isFalling)
            {
                bomb.StartFalling();  // Start falling this bomb
            }
        }
    }
}
