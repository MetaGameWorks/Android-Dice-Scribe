using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void _40k_stand_dice()
    {
        SceneManager.LoadScene("_40k_Stand_Dice");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
