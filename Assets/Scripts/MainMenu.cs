using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void OnclickPlay()
    {
        SceneManager.LoadScene("Pixel Adventure 2", LoadSceneMode.Single);
    }

    public void OnclickQuit()
    {
        Application.Quit();
    }

}
