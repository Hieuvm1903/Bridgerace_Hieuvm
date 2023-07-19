using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int height;
    public int width;
    public List<Vector2> listofposition = new List<Vector2>();
    public Dictionary<int, List<Vector2>> listofunbrick = new Dictionary<int, List<Vector2>>();
    public Dictionary<int, List<Brick>> listofbricks = new Dictionary<int, List<Brick>>();
    public List<Color> listofcolors = new List<Color>();
    public System.Random rand = new System.Random();
    public Transform tf;
    public void OnInit ()
    {
        tf = transform;      
        height = 9;
        width = 12;
        for(int  i = 0;i<width;i++)
        {
            for(int j = 0; j<height;j++)
            {
                listofposition.Add(new Vector2(i, j));
            }
        }

    }
    public void SpawnColor(int color)
    {
        
        for(int i = 0; i < height*width/Lvlmanager.Instance.listofcharacters.Count;i++)
        {
            int a = rand.Next(listofposition.Count);
            SpawnBrick(listofposition[a].x, listofposition[a].y, color);
            listofposition.RemoveAt(a);
        }

    }
    public void AddColor(Color color)
    {
        
        AddBrick();
        AddUnbrick();
        listofcolors.Add(color);
    }
    void AddBrick()
    {
        listofbricks.Add(listofcolors.Count, new List<Brick>());
    }
    void AddUnbrick()
    {
        listofunbrick.Add(listofcolors.Count, new List<Vector2>());
    }
    public Brick SpawnBrick(float x, float y,int color)
    {       
        Brick brick = Pooling.Instance.GetPooledObject();
        brick.tf.SetParent(tf);
        brick.tf.position = new Vector3(x, 0, y) + tf.position;
        brick.tf.rotation = tf.rotation;               
        brick.ChangeColor(listofcolors[color]);       
        listofbricks[color].Add(brick);
        return brick;

    }
    public void DelBrick(int color)
    {        
        for(int i = 0; i<listofbricks[color].Count; i++)
        {
            listofbricks[color][i].OnDespawn();           
        }
        listofbricks[color].Clear();       
        listofunbrick.Remove(color);

    }
    IEnumerator ISpawn(int color)
    {
        
        yield return null;
        List<Vector2> lv2 = listofunbrick[color];
        for (int i = 0; i < lv2.Count; i++)
        {
            SpawnBrick(lv2[i].x - tf.position.x, lv2[i].y - tf.position.z, color);
        }
        listofunbrick[color].Clear();

    }
    public void SpawnAfterFall(int color)
    {
        StartCoroutine(ISpawn(color));
    }

}
