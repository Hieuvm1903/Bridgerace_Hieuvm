using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public void EnterEnemy(Enemy enemy)
    {
        enemy.SetNextBrick();
    }
    public void OnExcute(Enemy enemy)
    {
        if (enemy.numbrick >= enemy.numstairs)
        {           
            enemy.ChangeState(new BuildState());           
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
