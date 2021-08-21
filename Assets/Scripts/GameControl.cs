using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    GameObject[] unit;
    GameObject[] enemy;
    GameObject[] UI;

    public bool isWin;
    private Text resultText;
    private GameObject resultWindow;


    // Start is called before the first frame update
    void Start()
    {
        resultText = GameObject.Find("ResultText").GetComponent<Text>();
        resultWindow = GameObject.Find("ResultWindow");
        resultWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        unit = GameObject.FindGameObjectsWithTag("Ally");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        UI = GameObject.FindGameObjectsWithTag("UI");


        if (Input.GetKeyDown(KeyCode.D))
        {

            resultWindow.SetActive(true);
            if (isWin)
            {
                resultText.text = "win";
            }
            Destroy();
        }
    }

    private void Destroy()
    {
        for (int i = 0; i < unit.Length; i++)
        {
            Destroy(unit[i]);
        }

        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }

        for (int i = 0; i < UI.Length; i++)
        {
            Destroy(UI[i]);
        }
    }
}
