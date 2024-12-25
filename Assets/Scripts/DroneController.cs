using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

Rigidbody rb;
float upDownAxis;
float forwardBackwardAxis;
float RightLeftAxis;
float forwardBackwardAngle = 0; 
float RightLeftAngle = 0; 
[SerializeField] 
float speed = 1.3f;
float angle = 25;
Animator anim;
bool isOnGround = false;



private   void Start() {

  rb = GetComponent<Rigidbody>();
  anim = GetComponent<Animator>();
  
}

 private void Update() 
 {
   Controlls();
   transform.localEulerAngles = Vector3.back * RightLeftAngle + Vector3.right * forwardBackwardAngle;
}
 private void FixedUpdate()
 {
   rb.AddRelativeForce(RightLeftAxis, upDownAxis, forwardBackwardAxis);
   
}
void Controlls(){
  if (Input.GetKey(KeyCode.Q))
  {
    upDownAxis = 10 * speed;
    anim.SetBool("Fly", true );
    isOnGround = false;
  }
  else if (Input.GetKey(KeyCode.E))
  {
    upDownAxis = 8;
    anim.SetBool("Fly", false);
    
  }
  else
   {
     upDownAxis = 9.81f;
     anim.SetBool("Fly", false);
  }
  
  if(Input.GetKey(KeyCode.W))
  {
    forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, angle , Time.deltaTime);
    forwardBackwardAxis = speed;
    anim.SetBool("Fly", true);

  }

  else if(Input.GetKey(KeyCode.S))
  {
    forwardBackwardAngle = Mathf.LerpUnclamped(forwardBackwardAngle, -angle, Time.deltaTime);
    forwardBackwardAxis = -speed;
    anim.SetBool("Fly", true);
  }

  else 
  {
    forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, 0, Time.deltaTime);
    forwardBackwardAxis = 0;
  }

   if(Input.GetKey(KeyCode.D))
  {
    RightLeftAngle = Mathf.Lerp(RightLeftAngle, angle , Time.deltaTime);
    RightLeftAxis = speed;
    anim.SetBool("Fly", true);

  }

  else if(Input.GetKey(KeyCode.A))
  {
    RightLeftAngle = Mathf.LerpUnclamped(RightLeftAngle, -angle, Time.deltaTime);
    RightLeftAxis = -speed;
    anim.SetBool("Fly", true);
  }

  else 
  {
    RightLeftAngle = Mathf.Lerp(RightLeftAngle, 0, Time.deltaTime);
    RightLeftAxis = 0;
}

if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
{
  forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, angle, Time.deltaTime);
  RightLeftAngle = Mathf.Lerp(RightLeftAngle, angle, Time.deltaTime);
  forwardBackwardAxis = 0.5f * speed;
  RightLeftAxis = 0.5f * speed;
}


if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
{
  forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, angle, Time.deltaTime);
  RightLeftAngle = Mathf.Lerp(RightLeftAngle, -angle, Time.deltaTime);
  forwardBackwardAxis = 0.5f * speed;
  RightLeftAxis = -0.5f * speed;
}
if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
{
  forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, -angle, Time.deltaTime);
  RightLeftAngle = Mathf.Lerp(RightLeftAngle, angle, Time.deltaTime);
  forwardBackwardAxis = -0.5f * speed;
  RightLeftAxis = 0.5f * speed;
}

if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
{
  forwardBackwardAngle = Mathf.Lerp(forwardBackwardAngle, -angle, Time.deltaTime);
  RightLeftAngle = Mathf.Lerp(RightLeftAngle, -angle, Time.deltaTime);
  forwardBackwardAxis = -0.5f * speed;
  RightLeftAxis = -0.5f * speed;
}
}

private void  OnCollisionEnter(Collision collision)
 {
  if (collision.gameObject.tag == "ground")
  {
    isOnGround = true;
  }
}

}

