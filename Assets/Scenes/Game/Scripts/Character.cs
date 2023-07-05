using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] public NavMeshAgent nav;
    [SerializeField] GameObject back;
    public Rigidbody rb;

    public LayerMask layer;
    protected string curani;
    public int numbrick;
    public SkinnedMeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    public void Addbrick(GameObject brick)
    {
        numbrick++;
        brick.transform.SetParent(back.transform);
        brick.transform.localPosition = new Vector3(0, numbrick*0.3f, 0);
        brick.transform.localRotation = Quaternion.Euler(0, 0, 0);

            
    }
    public void checkrbridge()
    {
        Debug.DrawRay(transform.position + rb.velocity * 0.2f + Vector3.up , Vector3.down*5f, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(transform.position + rb.velocity * 0.2f + Vector3.up, Vector3.down * 5f, out hit, 1f, layer))
        {
            Debug.Log("hit");
            Brick b = hit.collider.GetComponent<Brick>();
            if(b.GetComponent<MeshRenderer>().enabled == false || !b.matchcol(b.rend.material.color,rend.material.color))
            {
                if(numbrick >0)
                {
                    b.GetComponent<MeshRenderer>().enabled = true;
                    numbrick--;
                    b.changecolor(rend.material.color);
                    //go
                }
                else
                {
                    //stop
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick" )
        {
            Brick brick = other.GetComponent<Brick>();
            Color col = brick.rend.material.color;
            if (brick.matchcol(col, rend.material.color) || brick.matchcol(col, Color.gray))
            {
                float x = brick.transform.position.x;
                float y = brick.transform.position.z;
                Spawner map = brick.GetComponentInParent<Spawner>();
                Lvlmanager.Instance.setbrick(map, x, y);
                GetComponent<Character>().Addbrick(brick.gameObject);
            }
        }

    }
    public void Changeanim(string anime)
    {
        RuntimeAnimatorController newController = Resources.Load<RuntimeAnimatorController>("AnimationControllers/"+anime);

        if (newController != null)
        {
            // Change the animator controller
            animator.runtimeAnimatorController = newController;
        }
        else
        {
            Debug.LogError("Failed to load Animator Controller from Resources folder: " + anime);
        }


    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Character charac = collision.gameObject.GetComponent<Character>();
            if(numbrick>=charac.numbrick)
            {
                charac.fall();
            }
            else
            {
                fall();
            }

        }

    }
    public void fall()
    {
        Debug.Log("fall");
    }
  
}
