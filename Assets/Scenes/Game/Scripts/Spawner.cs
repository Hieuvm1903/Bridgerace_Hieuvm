using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int height;
    public int width;
    [SerializeField] Brick brick;
    public List<Color> colist;
    public List<int> numbrick;
    void Start()
    {

        height = 10;
        width = 12;
        colist.Add(Color.red);
        colist.Add(Color.blue);
        colist.Add(Color.yellow);
        colist.Add(Color.green);
        // StartCoroutine(Ispawn());
        Oninit();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Oninit ()
    {
        spawn();

    }
    void spawn()
    {
        for(int i =0; i<width; i++)
        {
            for(int j = 0; j<width; j++)
            {
                /*
                Brick sbrick = Instantiate(brick, new Vector3(i, 0, -j)+transform.position, this.transform.rotation, this.transform);
                int a = Random.Range(0, colist.Count);
                sbrick.changecolor(colist[a]);
                */
                Spawnbrick(i, -j);

            }
        }

    }
    IEnumerator Ispawn(float x, float y, float timer)
    {

        yield return new WaitForSeconds(timer);
        Spawnbrick(x, y);

    }
    public void Spawnbrick(float x, float y)
    {
        Brick sbrick = Instantiate(brick, new Vector3(x, 0, y)+transform.position, this.transform.rotation,this.transform);
        int a = Random.Range(0, colist.Count);
        sbrick.changecolor(colist[a]);
    }
    public void setbrick(float x, float y, float timer)
    {
        StartCoroutine(Ispawn(x,y,timer));
        
    }
    public void delbrick(Color col)
    {
        Brick[] lb = GetComponentsInChildren<Brick>();
        foreach (Brick b in lb)
        {
            if (b.matchcol(col, b.rend.material.color))
            {
                Destroy(b.gameObject);
            }
        }

    }
}
