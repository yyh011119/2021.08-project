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
        StartCoroutine("wave");
        StartCoroutine("wave2");
    }

    // Update is called once per frame
    void Update()
    {
        HpPercent = basecon.hpPercent;

        
    }
    
    public void enemycreate(int n)
    {
        Debug.Log("created!");
        GameObject go = GameObject.Instantiate(this.enemy[n]);
        go.transform.position = this.transform.Find("SpawnPoint").position;
    }

    IEnumerator wave()
    {

        while (waveMax > waveNumber)
        {
            enemycreate(enemyWave[waveNumber]);

            waveNumber++;


            yield return new WaitForSeconds(enemyTime[waveNumber]);
        }
    }

    IEnumerator wave2()
    {
        while (wave2Max > wave2Number)
        {
            if(HpPercent<0.5)
            {
                enemycreate(enemyWave2[wave2Number]);

                wave2Number++;


                yield return new WaitForSeconds(enemyTime2[wave2Number]);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
            
        }
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
