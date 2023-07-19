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
    const string MAIN_SCENE = "Main";
    private void Start()
    {
        OnInit();
       
    }
    
    // Update is called once per frame
    void Update()
    {        
        for(int i = 0;i< listofenemies.Count;i++)
        {
            Enemy enemy = listofenemies[i];
            if (enemy.currentstate != null)
            {
                enemy.currentstate.OnExcute(enemy);

            }
            
        }
    }
    public void OnInit()
    {
        Resume();
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
            enemy.SetNextBrick();
            enemy.ChangeState(new RunState());
        }

    }
    public void ActiveSpawner(int i, Character character)
    {
        if (i > 0)
        {
            listofspawners[i - 1].DelBrick(character.colorindex);
        }
        if (i  < listofspawners.Count)
        {
            character.colorindex = listofspawners[i].listofcolors.Count;
            listofspawners[i].AddColor(character.rend.material.color);
            listofspawners[i].SpawnColor(character.colorindex);

            if (character.GetComponent<Enemy>())
            {
                Enemy enemy = character.GetComponent<Enemy>();
                enemy.listdestinations.Clear();
                enemy.NewFloor();

            }
        }
        
    }
    IEnumerator Iwin()
    {
        
        yield return new WaitForSeconds(1f);
        Pause();
        if (Player.Instance.iswin)
            UIManager.Instance.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
        else
            UIManager.Instance.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString();
    }
    public void OpenWin()
    {
        StartCoroutine(Iwin());
    }
    public void RestartLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
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
