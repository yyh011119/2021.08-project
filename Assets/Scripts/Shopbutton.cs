using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopbutton : MonoBehaviour
{

    private int gold;
    private int upgrade;

    public string item;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gold = PlayerPrefs.GetInt("Gold");
        upgrade = PlayerPrefs.GetInt(item);


        cost = 50 + 25 * upgrade;
        buttonText();

        if(upgrade == 25)
        {
            this.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }


    public void buy()
    {
        if (!PlayerPrefs.HasKey(item))
        {
            PlayerPrefs.SetInt(item, 0);
        }

        if (gold >= cost)
        {
            PlayerPrefs.SetInt("Gold", gold - cost);
            upgrade++;
            PlayerPrefs.SetInt(item, upgrade);
        }
    }

    public void buttonText()
    {
        Text costText = this.transform.GetChild(0).transform.Find("Cost").GetComponent<Text>();
        costText.text = cost.ToString()+'g';

        Text upgradeText = this.transform.GetChild(0).transform.Find("Level").GetComponent<Text>();
        upgradeText.text = upgrade + "/25";
    }


    //public void buyCastleHp(int cost)
    //{
    //    if (!PlayerPrefs.HasKey("Upgrade_Hp"))
    //    {
    //        PlayerPrefs.SetInt("Upgrade_Hp", 0);
    //    }

    //    if (gold >= cost)
    //    {
    //        PlayerPrefs.SetInt("Gold", gold - cost);
    //        int upgrade = PlayerPrefs.GetInt("Upgrade_Hp");
    //        upgrade++;
    //        PlayerPrefs.SetInt("Upgrade_Hp", upgrade);
    //    }
    //}

    //public void buyPointSpeed(int cost)
    //{
    //    if (!PlayerPrefs.HasKey("Upgrade_Point"))
    //    {
    //        PlayerPrefs.SetInt("Upgrade_Point", 0);
    //    }

    //    if (gold >= cost)
    //    {
    //        PlayerPrefs.SetInt("Gold", gold - cost);
    //        int upgrade = PlayerPrefs.GetInt("Upgrade_Point");
    //        upgrade++;
    //        PlayerPrefs.SetInt("Upgrade_Point", upgrade);
    //    }
    //}
}
