using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generalScript : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private TMPro.TMP_Text endTxt;
    [SerializeField] CanvasGroup endScreenFade;

    public static bool isPause;
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
       
        if (!endGame() && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            pauseUI.SetActive(true);
            isPause = true;
        }
    }

    public bool endGame()
    {
        if (PlayerStatus.currHealth <= 0 || TimerScript.loseToTime)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (endScreenFade.alpha < 1)
            {
                endTxt.SetText("You are died...");
                endScreenFade.alpha += Time.deltaTime;
                return true;
            } else
            {

                Time.timeScale = 0;
                return true;
            }
        }

        if (BossScript.bossDed)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (endScreenFade.alpha < 1)
            {
                endTxt.SetText("Victory");
                endScreenFade.alpha += Time.deltaTime;
                return true;
            }
            else
            {

                Time.timeScale = 0;
                return true;
            }
        }

        return false;
    }

    public void resume()
    {
        Time.timeScale = 1;
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
    }

    public void quitGame()
    {
        questScript.state = 0;
        SceneManager.LoadScene(0);
    }
}
