using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    private GameObject shopWindow;
    private Text goldText;

    private int gold;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.SetInt("Gold", 0);
        }


        shopWindow = GameObject.Find("Canvas").transform.Find("Shop").gameObject;
        goldText = GameObject.Find("Gold").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        gold = PlayerPrefs.GetInt("Gold");
        goldText.text = ("Gold: " + gold);
    }

    

    public void stageScreen()
    {
        SceneManager.LoadScene("LevelScene");
    }

}
