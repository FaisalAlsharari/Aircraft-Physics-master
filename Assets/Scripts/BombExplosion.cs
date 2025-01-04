using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public GameObject explosionFX;
    public float expForce, radius;


   private void OnCollisionEnter(Collision other)
   {
    GameObject _exp = Instantiate(explosionFX, transform.position, transform.rotation);
    Destroy(_exp, 3);
    KnockBack();
    Destroy(gameObject);
   }


    void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearyby in colliders)
        {
            Rigidbody rig = nearyby.GetComponent<Rigidbody>();
            if(rig !=null)
            {
                rig.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }
 
}
