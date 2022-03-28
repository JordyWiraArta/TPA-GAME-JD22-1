using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject setting;


    public void Start()
    {
        main.SetActive(true);
        setting.SetActive(false);
    }

    public void playGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGameScene()
    {
        Application.Quit();
    }

    public void qualityChange(int qualityInt)
    {
        QualitySettings.SetQualityLevel(qualityInt);
    }

    public void fullScreenChange(bool fullScreenSet)
    {
        Screen.fullScreen = fullScreenSet;
    }

}
