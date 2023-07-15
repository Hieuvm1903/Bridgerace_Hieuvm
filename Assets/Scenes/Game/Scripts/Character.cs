using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject back;

    public bool iswin = false;
    public LayerMask layer;
    protected string curani;
    public int numbrick;
    public int onlvl;
    public int colorindex;
    public Collider collid;
    public SkinnedMeshRenderer rend;
    // Start is called before the first frame update
    public virtual void OnInit()
    {
        numbrick = 0;
        collid = GetComponent<CapsuleCollider>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Brick[] backbrick = back.GetComponentsInChildren<Brick>();
        foreach (Brick brick in backbrick)
        {
            brick.Ondespawn();
        }
    }
    public virtual void Addbrick(Brick brick)
    {
        
        

        brick.transform.SetParent(back.transform);
        brick.transform.localPosition = new Vector3(0, numbrick*0.2f, 0);
        brick.transform.localRotation = Quaternion.Euler(0, 0, 0);
        numbrick++;


    }
    public void CheckBuildBridge()
    {
        
            Debug.DrawRay(transform.position + transform.TransformDirection(Vector3.forward) * 0.7f + Vector3.up, Vector3.down * 5f, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward)*0.7f  + Vector3.up, Vector3.down * 5f, out hit, 5f, layer))
            {
            
                Stair stair = hit.collider.GetComponent<Stair>();
            if(stair != null)
                if (stair.mesh.enabled == false || !Match(stair.mesh.material.color, rend.material.color))
                {
                    if (numbrick > 0)
                    {
                    BuildStair(stair);
                    stair.OpenDoor(this);
                        if(stair.CompareTag("last"))
                        {
                        onlvl++;
                        Lvlmanager.Instance.ActiveSpawner(onlvl, this);
                        }   
                        if(stair.CompareTag("Finish"))
                        {
                        numbrick++;
                        }
                    }
                    else
                    {
                    stair.CloseDoor(this);
                    }
                }
 
            }
        
        
    }
    // Update is called once per frame
    void RemoveBrick()
    {
        Brick[] brick = GetComponentsInChildren<Brick>();
        numbrick--;
        brick[brick.Length - 1].Ondespawn();
     
    }    
    public void BuildStair(Stair stair)
    {
        stair.mesh.enabled = true;
        stair.ChangeColor(rend.material.color);
        RemoveBrick();
        SpawnNewBrick();
    }  
    public void SpawnNewBrick()
    {
        Spawner map = Lvlmanager.Instance.listofspawners[onlvl];       
        List<Vector2> lv2 = map.listofunbrick[colorindex];
        if (map.listofunbrick[colorindex].Count > 0)
        {
            int a = map.rand.Next(map.listofunbrick[colorindex].Count);
            map.Spawnbrick(lv2[a].x - map.transform.position.x, lv2[a].y - map.transform.position.z, colorindex);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick") )
        {

            Brick brick = other.GetComponent<Brick>();
            Color col = brick.rend.material.color;
            if( Match(col, Color.gray))
            {               
                brick.ChangeColor(rend.material.color);
                Addbrick(brick);

            }
            if (Match(col, rend.material.color) )
            {
                Transform brickpos = brick.transform;
                Lvlmanager.Instance.listofspawners[onlvl].listofunbrick[colorindex].Add(new Vector2(brickpos.position.x, brickpos.position.z));
                Lvlmanager.Instance.listofspawners[onlvl].listofbricks[colorindex].Remove(brick);
                Addbrick(brick);
            }
        }
        if(other.CompareTag("Finishbox"))
        {
            iswin = true;
           
            Lvlmanager.Instance.iswin = true;
            Lvlmanager.Instance.OpenWin();
        }        
        if(other.gameObject.layer == 3)
        {
            IgnoreCharacter(true);            
        }       
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Character>())
        {
            Character charac = collision.gameObject.GetComponent<Character>();
            if (numbrick > charac.numbrick)
            {
                charac.Fall();
            }
            else if(numbrick < charac.numbrick)
            {
                Fall();
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            IgnoreCharacter(false);
        }
        if (other.CompareTag("Finishbox"))
        {
            iswin = false;
            
        }
    }
    public void ChangeAnim(string anime)
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
    public void Fall()
    {
        numbrick = 0;
        IgnoreCharacter(true);
        StartCoroutine(Ignore());
        Brick[] backbrick = back.GetComponentsInChildren<Brick>();
        foreach(Brick brick in backbrick)
        {            
            brick.Faded();
            brick.transform.position = transform.position + 2*RandomUnitVector();           
            brick.transform.SetParent(Lvlmanager.Instance.transform);           
        }
        if(onlvl <= Lvlmanager.Instance.listofspawners.Count - 1 )
        Lvlmanager.Instance.listofspawners[onlvl].SpawnAfterFall(colorindex);        
    }    
    public bool Match(Color col1, Color col2)
    {
        
        var r = Mathf.Abs(col1.r - col2.r);
        var g = Mathf.Abs(col1.g - col2.g);
        var b = Mathf.Abs(col1.b - col2.b);
        var a = Mathf.Abs(col1.a - col2.a);
        return Mathf.Sqrt(r * r + g * g + b * b + a * a) < 0.5f;

    }
    public Vector3 RandomUnitVector()
    {
        float random = Random.Range(0f, 360f);
        return new Vector3(1.5f * Mathf.Cos(random), 0, 1.5f * Mathf.Sin(random));
    }
    void IgnoreCharacter(bool ignore)
    {
        for (int i = 0; i < Lvlmanager.Instance.listofcharacters.Count; i++)
        {
            Character character = Lvlmanager.Instance.listofcharacters[i];
            Physics.IgnoreCollision(GetComponent<Collider>(), character.GetComponent<Collider>(), ignore);
        }
    }
    IEnumerator Ignore()
    {       
        IgnoreCharacter(false);
        yield return new WaitForSeconds(1f);
    }
}
