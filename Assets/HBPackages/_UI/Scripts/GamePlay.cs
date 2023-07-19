using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : UICanvas
{
    
    public void WinButton()
    {
        
        SceneManager.LoadScene("Main");

        Close(0);
    }

    public void LoseButton()
    {
        SceneManager.LoadScene("Main");
        
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<Setting>();
    }
    
}
