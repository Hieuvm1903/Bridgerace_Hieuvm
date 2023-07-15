using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   // Start is called before the first frame update
    public interface IState
    {
        void EnterEnemy(Enemy enemy);


        void OnExcute(Enemy enemy);

        void OnExit(Enemy enemy);

    }

