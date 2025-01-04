using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject exp;  // Explosion prefab
    public float expForce, radius;  // Explosion force and radius

    public Rigidbody rb;
    public  bool isFalling = false;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();  // Ensure the Rigidbody is assigned
        }

        // Freeze position initially so the bomb doesn't fall
        rb.constraints = RigidbodyConstraints.FreezePosition;

        // Disable gravity initially
        rb.useGravity = false;
    }

    void Update()
    {
        // Each bomb will listen for the key presses from BombsController, so no need for key input here
    }

    // Start falling the bomb
    public void StartFalling()
    {
        if (!isFalling)
        {
            rb.useGravity = true; // Enable gravity
            rb.constraints = RigidbodyConstraints.None; // Remove position constraints
            isFalling = true;
        }
    }

    // Handle collision and explosion
    private void OnCollisionEnter(Collision other)
{
    // Check if it hit the ground or something solid
    if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy")) 
    {
        GameObject exp1 = Instantiate(exp, transform.position, transform.rotation);
        Destroy(exp1, 3);
        knockBack();
        Destroy(gameObject);
    }
}


    // Explosion force
    void knockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }
}
