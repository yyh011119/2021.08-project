using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public int unitType;
    public Vector3 unitPosition;

    private CreateUnit create;


    // Start is called before the first frame update
    void Start()
    {
        this.create = GameObject.Find("System(temp)").GetComponent<CreateUnit>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickButton()
    {
        create.unitcreate(unitType, unitPosition);
    }
}
