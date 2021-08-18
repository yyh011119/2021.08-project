using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(Resources.Load("Prefabs/Enemies/Enemy_1"), transform.Find("SpawnPoint").position, Quaternion.identity);
        }
    }

}
