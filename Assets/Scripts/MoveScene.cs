using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    public float mapLength;
    private float target_x;
    private float newPosition;

    // Start is called before the first frame update
    void Start()
    {
        //맵 길이 계산
        mapLength = GameObject.Find("AllyBase").transform.position.x - GameObject.Find("EnemyBase").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //타겟 좌표 계산
        target_x = GameObject.Find("Main Camera").transform.position.x;

        //카메라에 맞춰 배경 느리게 이동
        newPosition = Mathf.Lerp(this.gameObject.transform.position.x, target_x, 0.4f);
        transform.position = new Vector3(newPosition, 0, 2);
    }
}