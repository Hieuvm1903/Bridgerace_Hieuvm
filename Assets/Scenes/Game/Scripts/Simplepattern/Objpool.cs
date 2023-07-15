using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objpool : Singleton<Objpool>
{

    public List<GameObject> pool;
    public GameObject objtopool;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {

    }
    protected override void Awake()
    {
        /*
        amount = 200;
        for(int i = 0; i<amount;i++)
        {
            pool.Add(getobj());

        }
        */
        
        pool = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject getobj()
    {
        GameObject obj = Instantiate(objtopool, this.transform);
        obj.SetActive(false);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        //for (int i = 0; i < amount; i++)
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        GameObject obj = getobj();
        pool.Add(obj);
        return obj;
    }
    public void Returnpool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}
