using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEditor.PackageManager;

public class HelicopterController : MonoBehaviour
{
    Rigidbody rb;
    
    public BladesController MainBlads;
    public BladesController SubBlade;

    private float enginePower;
    public float EnginePower
    {
        get
        {
            return enginePower;
        }
        set 
        {
            MainBlads.BladeSpeed = value * 250;
            SubBlade.BladeSpeed = value * 500; 
            enginePower = value; 
        }
    }

    public float effectiveHeight;
    public float engineStartspeed;
    public float EngineLift = 0.0075f;
    public float ForwardForce;
    public float BackwardForce;
    public float TurnForce;
    public float TurnForcehelper = 1.5f;
    public float Forwardtiltforce;
    public float Turntiltforce;
    private Vector2 Movment = Vector2.zero;
    private Vector2 TILTING = Vector2.zero;
    public LayerMask groundLayer;
    private float distanceToground;
    public bool isOnGround = true;
    private float turning = 0f;
    public UnityEvent OnTakeOff;
    public UnityEvent OnLand;
    bool isFirstTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void Update()
    {
        HandleGroundcheck();
        HandleInputs();
        HandleEngine ();
        HandleInvoks();
      
    }


    protected void   FixedUpdate()
     {
        HelicopterHover();
        HelicopterMovements();
        HelicopterTilting();
        
    }

    void HandleInputs()
    {

        if(!isOnGround)
        {
             Movment.x = Input.GetAxis("Horizontal");
                Movment.y = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.C))
        {
            EnginePower -= EngineLift;
            if(EnginePower < 0) 
            {
                enginePower = 0;
            }
        }
          if(Input.GetAxis("Throttle") > 0 )
        {
            EnginePower += EngineLift;
        }else if(Input.GetAxis("Vertical") > 0 && !isOnGround)
        {
            EnginePower = Mathf.Lerp(EnginePower, 17.5f, 0.003f);
        }
        
        
        else if (Input.GetAxis("Horizontal") < 0.5f && !isOnGround)
        {
            EnginePower = Mathf.Lerp(EnginePower, 11f, 0.003f);
        }


        }else 
        {
            if (EnginePower > 7)
            {
                rb.AddForce(10 * transform.up, ForceMode.Acceleration);
            }
        }

       
    }



    void HandleGroundcheck()
    {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        Ray ray = new Ray(transform.position , direction);

        if(Physics.Raycast(ray, out hit, 3000 , groundLayer))
        {
            distanceToground = hit.distance;
            if(distanceToground < 2)
            {
                isOnGround = true;
            }else
            {
                isOnGround = false;
            }

            
        }

        
    }
    void HandleInvoks()
    {
        if(!isOnGround && isFirstTime)
        {
            OnTakeOff.Invoke();
        }else if(isOnGround && !isFirstTime)
        {
            OnLand.Invoke();
            isFirstTime = true;
        }
    }

    void HandleEngine()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                StartEngine();
            }else if(Input.GetKeyDown(KeyCode.Y) && isOnGround)
            {
                StopEngine();
            }
        }
    void HelicopterHover()
    {
        float upForce = 1 - Mathf.Clamp(rb.transform.position.y / effectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0,EnginePower, upForce) * rb.mass;
        rb.AddRelativeForce(Vector3.up * upForce); 
        
        
    }

    void HelicopterMovements()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(transform.forward * Mathf.Max(0f,Movment.y * ForwardForce * rb.mass));

        }else if(Input.GetAxis("Vertical") < 0)
            {
                rb.AddRelativeForce(-transform.forward * Mathf.Max(0, -Movment.y * BackwardForce * rb.mass));
            }
            float turn = TurnForce * Mathf.Lerp(Movment.x , Movment.x * (TurnForcehelper - Mathf.Abs(Movment.y)), Mathf.Max(0f, Movment.y));
            turning = Mathf.Lerp(turning,turn,Time.fixedDeltaTime * TurnForce);
            rb.AddRelativeTorque(0f, turning * rb.mass, 0f);
    }


        void HelicopterTilting()
        {
            TILTING.y = Mathf.Lerp(TILTING.y, Movment.y * Forwardtiltforce, Time.deltaTime);
            TILTING.x = Mathf.Lerp(TILTING.x, Movment.x * Turntiltforce, Time.deltaTime);
            rb.transform.localRotation = Quaternion.Euler(TILTING.y , rb.transform.localEulerAngles.y, -TILTING.x);
        }



        public void StartEngine()
        {
            DOTween.To(Starting,0,8.0f, engineStartspeed);
        }

        void Starting(float value)
        {
            EnginePower = value;
        }

        public void StopEngine()
        {
            DOTween.To(Stopping,EnginePower,0.0f,engineStartspeed);
        }

        void Stopping(float value)
        {
            EnginePower = value;
        }
    }

