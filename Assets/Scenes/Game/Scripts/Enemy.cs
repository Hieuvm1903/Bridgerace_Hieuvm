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
   
    protected override void AddBrick(Brick brick)
    {

        
        base.AddBrick(brick);

        if (numbrick < numstairs - 1)
        {
            SetNextBrick();
        }
        else
        {
            ChangeState(new BuildState());
        }
    }
    public override void OnInit()
    {
        onlvl = 0;
        numbrick = 0;
        iswin = false;
        base.OnInit();
        ChangeAnim("Run");       
        nav.speed = 3f;
        numstairs = 10;
        NewFloor();
    }
    public void NewFloor()
    {
        listdestinations = Lvlmanager.Instance.listofspawners[onlvl].listofbricks[colorindex];
    }
    public void SetNextBrick()
    {
                   
            Sort();
            if (listdestinations.Count > 0)
            { 
                SetDestination(listdestinations[0].tf.position); 
            }

        
    }
    public void Sort()
    {
        listdestinations.Sort((x, y) => Vector3.Distance(x.tf.position,tf.position).CompareTo(Vector3.Distance(y.tf.position, tf.position)));        
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
