using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] enemy; //unit 종류

    public int[] enemyWave;
    public float[] enemyTime;
    public int[] enemyWave2;
    public float[] enemyTime2;

    private int waveNumber = 0, wave2Number=0, waveMax, wave2Max;
    private float time=0 , time2=0, timet; //  유닛소환용, 타이머용
    private int m; // 분
    private Base basecon;
    private float HpPercent;

    // Start is called before the first frame update
    void Start()
    {
        waveMax = Mathf.Min(enemyTime.Length, enemyWave.Length);
        wave2Max = Mathf.Min(enemyTime2.Length, enemyWave2.Length) ;
        basecon = GameObject.Find("EnemyBase").GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        HpPercent = basecon.hpPercent;


        if(waveMax>waveNumber)
        {
            if (time >= enemyTime[waveNumber])
            {
                enemycreate(enemyWave[waveNumber]);

                waveNumber++;
                time = 0;
            }
        }

        if (wave2Max > wave2Number && HpPercent < 0.5)
        {
            if (time2 >= enemyTime2[wave2Number])
            {
                enemycreate(enemyWave2[wave2Number]);

                wave2Number++;
                time2 = 0;
            }
        }

    }
    
    public void enemycreate(int n)
    {
        Debug.Log("created!");
        GameObject go = GameObject.Instantiate(this.enemy[n]);
        go.transform.position = this.transform.Find("SpawnPoint").position;
    }

    /*
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
    */
}
