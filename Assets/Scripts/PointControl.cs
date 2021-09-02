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
        checkTime -= PlayerPrefs.GetInt("Upgrade_Point") * 0.01f;
        StartCoroutine(PointUpdate(checkTime));
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

    public IEnumerator PointUpdate(float checkTime)
    {
        while(point<=maxPoint)
        {
            point++;
            yield return new WaitForSeconds(checkTime);
        }

    }

    public void PointCheck()
    {
        if (point > maxPoint) point = maxPoint;
    }

}
