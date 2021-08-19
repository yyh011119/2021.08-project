using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PointControl : MonoBehaviour
{
    public int point;
    public int maxPoint;
    public GUIStyle style;

    private Text pointText;
    private float time;
    private float checkTime;


    // Start is called before the first frame update
    void Start()
    {
        pointText = GameObject.Find("Point").GetComponent<Text>();
        checkTime = 0.1f;
        maxPoint = 50;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > checkTime && point < maxPoint)
        {
            point++;
            time = 0;
        }


        pointcounter();
    }

    void OnGUI()
    {

    }

    void pointcounter()
    {
        pointText.text = point.ToString() + "/" + maxPoint.ToString();
    }


}
