using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 10f;

    private Rigidbody rb;



    void Start () 
    {
       rb = GetComponent<Rigidbody>();
       

       Destroy(gameObject, lifeTime);
    }

     private void OnCollisionEnter(Collision other)
      {
        Destroy(gameObject);
    }
}
