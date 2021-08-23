using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    private GameObject pauseWindow;
    private MoveCamera camera;


    // Start is called before the first frame update
    void Start()
    {
        pauseWindow = GameObject.Find("Canvas").transform.Find("PauseWindow").gameObject;
        camera = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
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
        camera.enabled = false;
    }

    public void resumeButton()
    {
        Time.timeScale = 1;
        pauseWindow.SetActive(false);
        camera.enabled = true;
    }
    
    public void restartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BattleScene");
    }

    public void mainScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }


}
