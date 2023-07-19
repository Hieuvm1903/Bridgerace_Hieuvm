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
    protected List<Brick> brickinback = new List<Brick>();
    WaitForSeconds timetowait = new WaitForSeconds(1f);
    [SerializeField] protected Transform tf;
    [SerializeField] Transform backtransform;
    const string FINISH_BOX = "Finishbox";
    const string FINISH = "Finish";
    const string LAST = "last";
    const string BRICK = "Brick";



    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Character>())
        {
            Character charac = collision.gameObject.GetComponent<Character>();
            if (numbrick > charac.numbrick)
            {
                charac.Fall();
            }
            else if (numbrick < charac.numbrick)
            {
                Fall();
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BRICK))
        {

            Brick brick = other.GetComponent<Brick>();
            Color col = brick.rend.material.color;
            if (Match(col, Color.gray))
            {
                brick.ChangeColor(rend.material.color);
                AddBrick(brick);

            }
            if (Match(col, rend.material.color))
            {
                Lvlmanager.Instance.listofspawners[onlvl].listofunbrick[colorindex].Add(new Vector2(brick.tf.position.x, brick.tf.position.z));
                Lvlmanager.Instance.listofspawners[onlvl].listofbricks[colorindex].Remove(brick);
                AddBrick(brick);
            }
        }
        if (other.CompareTag(FINISH_BOX))
        {
            iswin = true;
            ChangeAnim("Fall");
            Lvlmanager.Instance.OpenWin();
        }
        if (other.gameObject.layer == 3)
        {
            IgnoreCharacter(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            IgnoreCharacter(false);
        }
        if (other.CompareTag(FINISH_BOX))
        {
            iswin = false;

        }
    }
    
    private void Start()
    {
        backtransform = back.transform;
        tf = transform;
    }
    public virtual void OnInit()
    {
        
        numbrick = 0;
        onlvl = 0;
    }
    protected virtual void AddBrick(Brick brick)
    {
        brick.tf.SetParent(backtransform);
        brick.tf.localPosition = new Vector3(0, numbrick*0.2f, 0);
        brick.tf.localRotation = Quaternion.Euler(0, 0, 0);
        brickinback.Add(brick);
        numbrick++;
    }
    public void CheckBuildBridge()
    {
        
            Debug.DrawRay(tf.position + tf.TransformDirection(Vector3.forward) * 0.75f + Vector3.up, Vector3.down * 5f, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(tf.position + tf.TransformDirection(Vector3.forward)*0.75f  + Vector3.up, Vector3.down * 5f, out hit, 5f, layer))
            {
            
                Stair stair = hit.collider.GetComponent<Stair>();
            if(stair != null)
                if (stair.mesh.enabled == false || !Match(stair.mesh.material.color, rend.material.color))
                {
                    if (numbrick > 0)
                    {
                    BuildStair(stair);
                    stair.OpenDoor(this);
                        if(stair.CompareTag(LAST))
                        {
                        onlvl++;
                        Lvlmanager.Instance.ActiveSpawner(onlvl, this);
                        }   
                        if(stair.CompareTag(FINISH))
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
    protected void RemoveBrick()
    {       
        numbrick--;
        brickinback[numbrick].OnDespawn();
        brickinback.RemoveAt(numbrick);
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
            map.SpawnBrick(lv2[a].x - map.tf.position.x, lv2[a].y - map.tf.position.z, colorindex);
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
        for(int i = 0;i<brickinback.Count;i++)
        {
            Brick brick = brickinback[i];
            brick.Faded();
            brick.tf.position = tf.position + 1.5f * RandomUnitVector();
            brick.tf.SetParent(Lvlmanager.Instance.transform);
        }
        brickinback.Clear();
        if(onlvl < Lvlmanager.Instance.listofspawners.Count  )
        Lvlmanager.Instance.listofspawners[onlvl].SpawnAfterFall(colorindex);        
    }    
    public bool Match(Color col1, Color col2)
    {
        
        var r = Mathf.Abs(col1.r - col2.r);
        var g = Mathf.Abs(col1.g - col2.g);
        var b = Mathf.Abs(col1.b - col2.b);
        var a = Mathf.Abs(col1.a - col2.a);
        return Mathf.Sqrt(r * r + g * g + b * b + a * a) < 0.2f;

    }
    public Vector3 RandomUnitVector()
    {
        float random = Random.Range(0f, 360f);
        return new Vector3(1.5f * Mathf.Cos(random), 0, 1.5f * Mathf.Sin(random));
    }
    protected void IgnoreCharacter(bool ignore)
    {
        for (int i = 0; i < Lvlmanager.Instance.listofcharacters.Count; i++)
        {
            Character character = Lvlmanager.Instance.listofcharacters[i];
            Physics.IgnoreCollision(collid, character.collid, ignore);
        }
    }
    IEnumerator Ignore()
    {               
        yield return timetowait ;
        IgnoreCharacter(false);
    }
}
