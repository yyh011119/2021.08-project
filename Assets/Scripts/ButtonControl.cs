using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public int unitType;
    private CreateUnit create;


    // Start is called before the first frame update
    void Start()
    {
        this.create = GameObject.Find("AllyBase").GetComponent<CreateUnit>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickButton()
    {
        create.unitCreate(unitType);
    }

    public void UpgradeButton()
    {
        this.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
