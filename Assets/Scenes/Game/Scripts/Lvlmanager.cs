using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvlmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Lvlmanager instance;
    public static Lvlmanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Lvlmanager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Lvlmanager>();
                }
            }
            return instance;
        }
    }
    public List<Spawner> Lspawner;
    void Start()
    {
        Spawner map1 = Lspawner[0];
        map1.Oninit();
        Spawner map2 = Lspawner[1];
        map2.Oninit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setbrick(Spawner spawner, float x, float y)
    {
        float xs = spawner.transform.position.x;
        float zs = spawner.transform.position.z;

        float timer = Random.Range(3f, 7f);
        spawner.setbrick(x-xs,y-zs,timer);

    }
}
