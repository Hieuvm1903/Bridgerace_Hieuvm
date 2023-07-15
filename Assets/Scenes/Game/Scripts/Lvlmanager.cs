using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvlmanager : Singleton<Lvlmanager>
{
    // Start is called before the first frame update

    public List<Spawner> listofspawners;
    public GameObject finishbox;
    public List<Enemy> listofenemies  ;
    public List<Character> listofcharacters;
    public bool iswin;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        Resume();
        listofenemies = FindObjectsOfType<Enemy>().ToList();
        listofcharacters = FindObjectsOfType<Character>().ToList();
        Spawner map1 = listofspawners[0];
        for (int i = 0; i < listofspawners.Count; i++)
        {
            listofspawners[i].OnInit();
        }
        for (int i = 0; i < listofcharacters.Count; i++)
        {
            Character character = listofcharacters[i];
            character.colorindex = i;           
            map1.AddColor(character.rend.material.color);
            map1.SpawnColor(i);

        }
        for (int i = 0; i < listofenemies.Count; i++)
        {
            Enemy enemy = listofenemies[i];
            enemy.OnInit();
            enemy.Setposition();
            enemy.ChangeState(new RunState());
        }
        //Camfl.Instance.settarget(Player.Instance.transform);
    }
    // Update is called once per frame
    void Update()
    {
        if(!iswin)
        for(int i = 0;i< listofenemies.Count;i++)
        {
            Enemy enemy = listofenemies[i];
            if (enemy.currentstate != null)
            {
                enemy.currentstate.OnExcute(enemy);

            }
            
        }
    }
    public void ActiveSpawner(int i, Character character)
    {
        if (i > 0)
        {
            listofspawners[i - 1].DelBrick(character.colorindex);
        }
        if (i + 1 <= listofspawners.Count)
        {
            character.colorindex = listofspawners[i].listofcolors.Count;
            listofspawners[i].AddColor(character.rend.material.color);
            listofspawners[i].SpawnColor(character.colorindex);

            if (character.GetComponent<Enemy>())
            {
                Enemy enemy = character.GetComponent<Enemy>();
                enemy.listdestinations.Clear();
                enemy.NextFloor();

            }
        }
        
    }
    IEnumerator Iwin()
    {
        Pause();
        if (Player.Instance.iswin)
            UIManager.Instance.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
        else
            UIManager.Instance.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
        yield return new WaitForSeconds(3f);
    }
    public void OpenWin()
    {
        StartCoroutine(Iwin());
    }
    public void RestartLevel()
    {
        // Get the current scene's name or index
        SceneManager.LoadScene("Main");
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }

}
