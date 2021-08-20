using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnit : MonoBehaviour
{

    public GameObject[] unit;
    public int[] cost;
    private PointControl pointcontrol;


    // Start is called before the first frame update
    void Start()
    {
        this.pointcontrol = GameObject.Find("System(temp)").GetComponent<PointControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void unitcreate(int n)
    {
        if (pointcontrol.point >= cost[n])
        {
            GameObject go = GameObject.Instantiate(this.unit[n]);
            go.transform.position = this.transform.Find("SpawnPoint").position;
            pointcontrol.point -= cost[n];
        }
    }

}

