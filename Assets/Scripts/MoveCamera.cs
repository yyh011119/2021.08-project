using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float backgroundSpeed;
    public float mapLength;

    // Start is called before the first frame update
    void Start()
    {
        //맵 길이 계산
        backgroundSpeed = 10f;
        mapLength = GameObject.Find("EnemyBase").transform.position.x - GameObject.Find("AllyBase").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 좌우키로 이동
        if (Input.GetKey(KeyCode.RightArrow) && this.gameObject.transform.position.x < (mapLength - 20) / 2)
        {
            transform.position += new Vector3(backgroundSpeed*Time.deltaTime, 0, 0) ;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && this.gameObject.transform.position.x > -(mapLength - 20) / 2)
        {
            transform.position += new Vector3(-backgroundSpeed * Time.deltaTime, 0, 0);
        }

        //좌표 계산 및 출력
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown("o"))
        {
            Debug.Log(pz);
        }
    }
}
