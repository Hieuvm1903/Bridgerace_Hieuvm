using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : Singleton<Pooling>
{
    public List<Brick> pool;
    public Brick bricktopool;
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
            pool.Add(GetBrick());

        }
        */
        pool = new List<Brick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Brick GetBrick()
    {
        Brick brick = Instantiate(bricktopool,this.transform);
        //brick.gameObject.SetActive(false);
        
        return brick;
    }
    public Brick GetPooledObject()
    {
        /*
        //for (int i = 0; i < amount; i++)
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        */
        if(pool.Count == 0)
        {
            Brick brick = GetBrick();
            brick.gameObject.SetActive(true);
            
            return brick;

        }
        Brick tempbrick = pool[0];
        tempbrick.gameObject.SetActive(true);
        pool.Remove(pool[0]);
        return tempbrick;
    }
    public void BackToPool(Brick brick)
    {
       
        brick.gameObject.SetActive(false);
        
        pool.Add(brick);
        brick.transform.SetParent(transform);
    }
}
