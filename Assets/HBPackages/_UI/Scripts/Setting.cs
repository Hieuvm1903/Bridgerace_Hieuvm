using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{

    public void PauseGame()
    {
        Time.timeScale = 0;
        Close(0);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Close(0);
    }
}
