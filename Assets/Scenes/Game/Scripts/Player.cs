using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody), typeof (BoxCollider))]
public class Player : Character
{  public static Player player;
    [SerializeField] DynamicJoystick djoystick;
    [SerializeField] float speed ;
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
        

    }

    private void Update()
    {
        
        checkrbridge();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(djoystick.Horizontal*speed, rb.velocity.y, djoystick.Vertical*speed);       
        
        if (djoystick.Horizontal != 0 || djoystick.Vertical != 0)
        {
            
            Changeanim("Run");
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            Changeanim("Idle");
        }
    }




}
