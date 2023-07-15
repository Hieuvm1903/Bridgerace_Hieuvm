using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody), typeof (CapsuleCollider))]
public class Player : Character
{  private static Player player;
    [SerializeField] DynamicJoystick djoystick;
    [SerializeField] float speed ;
    public Rigidbody rb;
    public static Player Instance
    {
        get
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
                if (player == null)
                {
                    player = new GameObject().AddComponent<Player>();
                }
            }
            return player;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        djoystick = FindObjectOfType<DynamicJoystick>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if( rb.velocity.z>0)
        {
            CheckBuildBridge();
        }
        
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(djoystick.Horizontal*speed, rb.velocity.y, djoystick.Vertical*speed);
        velocity = AdjustVelocityToSlope(velocity);
        rb.velocity = velocity;
        if (djoystick.Horizontal != 0 || djoystick.Vertical != 0)
        {
            ChangeAnim("Run");
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            if (iswin)
                ChangeAnim("Fall");
            else
                ChangeAnim("Idle");
        }
    }
    public Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }



}
