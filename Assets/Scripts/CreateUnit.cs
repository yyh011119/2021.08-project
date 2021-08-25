using UnityEngine;

public class CreateUnit : PointControl
{

    public GameObject[] unit;
    public int[] cost;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public void unitCreate(int n)
    {
        if (point >= cost[n])
        {
            GameObject go = GameObject.Instantiate(this.unit[n]);
            go.transform.position = this.transform.Find("SpawnPoint").position;
            point -= cost[n];
        }
    }

}

