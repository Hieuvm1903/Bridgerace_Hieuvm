using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   // Start is called before the first frame update
    public interface IState
    {
        void Enterenemy(Enemy enemy);


        void Onexcute(Enemy enemy);

        void Onexit(Enemy enemy);

    }

