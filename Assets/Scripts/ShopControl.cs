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

        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("sssssssssssssss");
            PlayerPrefs.SetInt("Gold", gold + 100);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("sssssssssssssss");
            PlayerPrefs.DeleteAll();
        }

    }

    public void buy(int cost)
    {
        if (!PlayerPrefs.HasKey("Upgrade_Hp"))
        {
            PlayerPrefs.SetInt("Upgrade_Hp", 0);
        }

        if(gold>=cost)
        {
            PlayerPrefs.SetInt("Gold", gold - cost);
            int upgrade = PlayerPrefs.GetInt("Upgrade_Hp");
            upgrade++;
            PlayerPrefs.SetInt("Upgrade_Hp", upgrade);
        }
    }

    public void stageScreen()
    {
        SceneManager.LoadScene("LevelScene");
    }

}
