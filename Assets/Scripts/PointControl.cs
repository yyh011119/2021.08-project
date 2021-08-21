using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PointControl : MonoBehaviour
{
    public int point;
    public int maxPoint;
    public float checkTime;
    public GUIStyle style;

    private Text pointText;
    private float time;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pointText = GameObject.Find("Point").GetComponent<Text>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        PointCheck();
        PointCounter();
    }

    void OnGUI()
    {

    }

    void PointCounter()
    {
        pointText.text = point.ToString() + "/" + maxPoint.ToString();
    }

    public void PointCheck()
    {
        time += Time.deltaTime;
        if (time > checkTime && point < maxPoint)
        {
            point++;
            time = 0;
        }
        if (point > maxPoint) point = maxPoint;
    }

}
