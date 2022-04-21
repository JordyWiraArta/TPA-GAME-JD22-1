using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generalScript : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseUI;


    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            pauseUI.SetActive(true);
        }
    }

    public void resume()
    {
        Time.timeScale = 1;
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
    }
}
