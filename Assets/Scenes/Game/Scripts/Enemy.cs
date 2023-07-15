using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] public NavMeshAgent nav;
    public List<Brick> listdestinations;    
    public IState currentstate;
    public int numstairs;


    // Start is called before the first frame update
    
 
    public override void OnInit()
    {
        onlvl = 0;
        numbrick = 0;
        iswin = false;
        base.OnInit();
        ChangeAnim("Run");
        
        nav.speed = 3f;
        numstairs = 10;
        NextFloor();
        

    }
    public void NextFloor()
    {
        listdestinations = Lvlmanager.Instance.listofspawners[onlvl].listofbricks[colorindex];
    }
    // Update is called once per frame
   public void Setposition()
    {
                   
            Sort();
            if (listdestinations.Count > 0)
            { 
                SetDestination(listdestinations[0].transform.position); 
            }

        
    }
    public override void Addbrick(Brick brick)
    {      
        base.Addbrick(brick);
    }
    public void Sort()
    {
        listdestinations.Sort((x, y) => Vector3.Distance(x.transform.position,transform.position).CompareTo(Vector3.Distance(y.transform.position, transform.position)));
        listdestinations.Sort((x, y) => x.gameObject.activeInHierarchy.CompareTo(y.gameObject.activeInHierarchy));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Brick"))
        {

            Brick brick = other.GetComponent<Brick>();
            Color col = brick.rend.material.color;
            if(Match(col, Color.gray))
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
                if (numbrick < numstairs-1)
                {
                    Setposition();
                }
                else
                {
                    ChangeState(new BuildState());
                }
                
            }
        }
        if (other.CompareTag("Finishbox"))
        {
            iswin = true;
            
            Lvlmanager.Instance.iswin = true;
            Lvlmanager.Instance.OpenWin();
        }
    }
    public void SetDestination(Vector3 pos)
    {
        nav.SetDestination(pos);
    }
    public void ChangeState(IState newstate)
    {
        
        if (currentstate != null)
        {
            currentstate.OnExit(this);
        }
        currentstate = newstate;
        if (currentstate != null)
        {
            currentstate.EnterEnemy(this);
        }
        

    }
}
