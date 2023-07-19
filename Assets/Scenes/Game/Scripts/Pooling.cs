using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : Singleton<Pooling>
{
    public List<Brick> pool = new List<Brick>();
    public Brick bricktopool;
    public int amount;


    public Brick GetBrick()
    {
        Brick brick = Instantiate(bricktopool,transform);       
        
        return brick;
    }
    public Brick GetPooledObject()
    {        
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
        brick.tf.SetParent(transform);
    }
}
