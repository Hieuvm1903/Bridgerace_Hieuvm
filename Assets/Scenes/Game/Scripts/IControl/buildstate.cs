using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{

    public void EnterEnemy(Enemy enemy)
    {
        enemy.SetDestination(Lvlmanager.Instance.finishbox.transform.position);
    }
    public void OnExcute(Enemy enemy)
    {

        if (enemy.numbrick == 0)
        {          
            enemy.ChangeState(new RunState());
        }
        else
        {

            enemy.CheckBuildBridge();
          
        }
        if(enemy.iswin)
        {
            enemy.ChangeAnim("Fall");
        }
       
            
        

    }
    public void OnExit(Enemy enemy)
    {

    }
}
