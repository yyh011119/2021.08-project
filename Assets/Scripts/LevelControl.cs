using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    private GameObject levelWindow;
    private int level;
    private int maxlevel;
    private Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        level = PlayerPrefs.GetInt("Level");
        levelWindow = GameObject.Find("Canvas").transform.Find("Stage").gameObject;
        goldText = GameObject.Find("Gold").GetComponent<Text>();


        maxlevel = 2;
        levelLock(maxlevel);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("sssssssssssssss");
            PlayerPrefs.DeleteAll();
        }
        if(PlayerPrefs.HasKey("Gold"))
        {
            goldText.text = ("Gold: " + PlayerPrefs.GetInt("Gold"));
        }

    }

    public void levelCheck(int n)
    {
        
        if (n <= level && n<=maxlevel)
        {
            SceneManager.LoadScene(n+1);
        }
    }

    public void levelLock(int n)
    {
        while(n>=0)
        {
            if(n+1>level)
            {
                Text text=levelWindow.transform.GetChild(n).transform.Find("Text").GetComponent<Text>();
                text.text = "X";
            }
            n--;
        }
    }

    public void goShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

}
