using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] enemy; //unit 종류

    public int[] enemyUnit;
    public float[] enemyTime;

    private int waveNumber,waveMax;
    private float time, timet; //  유닛소환용, 타이머용
    private Text timer;
    private int m; // 분

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Text>();
        waveMax = enemyTime.Length;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if(waveMax<=waveNumber)
        {
            //Debug.Log("aaaaaaaaaaaaaaaaaa");
        }
        else if (time >= enemyTime[waveNumber])
        {
            enemycreate(enemyUnit[waveNumber]);

            waveNumber++;
            time = 0;
        }

        timet += Time.deltaTime;
        showTime(timet);


    }

    public void enemycreate(int n)
    {
        GameObject go = GameObject.Instantiate(this.enemy[n]);
        go.transform.position = this.transform.Find("SpawnPoint").position;
    }

    void showTime(float t)
    {
        m = 0;
        while(t>=60)
        {
            t -= 60;
            m++;
        }

        t= t-t % 1;
        timer.text = m.ToString()+":"+t.ToString("N0");
    }
}
