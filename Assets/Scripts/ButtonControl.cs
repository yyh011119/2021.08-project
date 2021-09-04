using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public int unitType;
    private CreateUnit create;


    // Start is called before the first frame update
    void Start()
    {
        this.create = GameObject.Find("AllyBase").GetComponent<CreateUnit>();
        costText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickButton()
    {
        create.unitCreate(unitType);
    }

    public void UpgradeButton(int cost)
    {
        Debug.Log(create.point);

        if(create.point >= cost)
        {
            create.point -= cost;
            this.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void costText()
    {
        Text cost = this.transform.Find("Cost").GetComponent<Text>();
        cost.text = create.cost[unitType].ToString();
    }
}
