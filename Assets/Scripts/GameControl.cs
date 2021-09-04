using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    private GameObject pauseWindow;
    private MoveCamera cam;


    // Start is called before the first frame update
    void Start()
    {
        pauseWindow = GameObject.Find("Canvas").transform.Find("PauseWindow").gameObject;
        cam = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseButton()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        pauseWindow.transform.SetAsLastSibling();
        cam.enabled = false;
    }

    public void resumeButton()
    {
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        cam.enabled = true;
    }
    
    public void restartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void mainScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    public void stageScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelScene");
    }

}
