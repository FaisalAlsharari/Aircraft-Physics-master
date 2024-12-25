using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BladesController : MonoBehaviour
{
    public enum Axis
    {
        x,y,Z
    }
    public Axis rotationAxis;
    private float bladeSpeed;
    public float BladeSpeed 
    {
        get{
            return bladeSpeed;
        }set{
            bladeSpeed = Mathf.Clamp(value,0,3000);
        }
    }
    
    public bool inverseRotation;
    private Vector3 Rotation;
    float rotateDegree;


     void Start() 
     {
        Rotation = transform.localEulerAngles;
    }

     void Update() 
    {
        if (inverseRotation)
        {
            rotateDegree -= bladeSpeed * Time.deltaTime;

        }else 
        {
            rotateDegree += bladeSpeed * Time.deltaTime;
            rotateDegree = rotateDegree%360;

             switch (rotationAxis)
            {

            case Axis.y:
              transform.localRotation = Quaternion.Euler(Rotation.x, rotateDegree, Rotation.z);
              break;
            case Axis.Z:
              transform.localRotation = Quaternion.Euler(Rotation.x, Rotation.y , rotateDegree);
              break;
            default:
            transform.localRotation = Quaternion.Euler(rotateDegree, Rotation.y, Rotation.z);
            break;



            }
           
        }

        

        
    }

    
}
