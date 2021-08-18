using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnit : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Resources.Load("Prefabs/Units/unit_1"), transform.Find("SpawnPoint").position, Quaternion.identity);
        }
    }

}

