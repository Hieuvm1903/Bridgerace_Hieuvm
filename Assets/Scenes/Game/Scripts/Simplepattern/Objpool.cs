using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objpool : Singleton<Objpool>
{

    public List<GameObject> pool = new List<GameObject>();
    public GameObject objtopool;
    public int amount;
    // Start is called before the first frame update
    public GameObject GetObj()
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
        GameObject obj = GetObj();
        pool.Add(obj);
        return obj;
    }
    public void ReturnPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}
