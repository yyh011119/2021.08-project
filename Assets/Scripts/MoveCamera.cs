using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    public float mapLength;

    // Start is called before the first frame update
    void Start()
    {
        //맵 길이 계산
        mapLength = GameObject.Find("AllyBase").transform.position.x - GameObject.Find("EnemyBase").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 좌우키로 이동
        if (Input.GetKey(KeyCode.RightArrow) && this.gameObject.transform.position.x < (mapLength - 20) / 2)
        {
            transform.position += new Vector3(backgroundSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && this.gameObject.transform.position.x > -(mapLength - 20) / 2)
        {
            transform.position += new Vector3(-backgroundSpeed, 0, 0);
        }

        //좌표 계산 및 출력
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown("o"))
        {
            Debug.Log(pz);
        }
    }
}
