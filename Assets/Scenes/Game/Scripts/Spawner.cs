using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int height;
    public int width;
    public List<Vector2> listofposition;
    public Dictionary<int, List<Vector2>> listofunbrick;
    public Dictionary<int, List<Brick>> listofbricks;
    public List<Color> listofcolors;
    public System.Random rand = new System.Random();
    void Update()
    {

    }
    public void OnInit ()
    {
       
        listofunbrick = new Dictionary<int, List<Vector2>>();
        listofbricks = new Dictionary<int, List<Brick>>();
        listofcolors = new List<Color>();
        height = 9;
        width = 12;
        listofposition = new List<Vector2>();
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
            Spawnbrick(listofposition[a].x, listofposition[a].y, color);
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
    public Brick Spawnbrick(float x, float y,int color)
    {       
        Brick brick = Pooling.Instance.GetPooledObject();
        brick.transform.SetParent(transform);
        brick.transform.position = new Vector3(x, 0, y) + transform.position;
        brick.transform.rotation = transform.rotation;               
        brick.ChangeColor(listofcolors[color]);       
        listofbricks[color].Add(brick);
        return brick;

    }
    public void DelBrick(int color)
    {

        
        for(int i = 0; i<listofbricks[color].Count; i++)
        {
            listofbricks[color][i].Ondespawn();
            
        }

        listofbricks[color].Clear();       
        listofunbrick.Remove(color);

    }
    IEnumerator ISpawn(int color)
    {
        List<Vector2> lv2 = listofunbrick[color];
        for (int i = 0; i < lv2.Count; i++)
        {
            Spawnbrick(lv2[i].x - transform.position.x, lv2[i].y - transform.position.z, color);

        }
        listofunbrick[color].Clear();
        yield return new WaitForSeconds(0f);
        
    }
    public void SpawnAfterFall(int color)
    {
        StartCoroutine(ISpawn(color));
    }

}
