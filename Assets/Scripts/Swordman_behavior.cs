using UnityEngine;

public class Swordman_behavior : MonoBehaviour
{
    private float swordman_moveSpeed;
    public bool ally;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Determine_Ally();
    }

    // Update is called once per frame
    void Update()
    {
        UnitMove();
        UnitAnime();
    }

    void Determine_Ally()
    {
        //아군 방향
        if (ally == true)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            swordman_moveSpeed = -2f;
        }

        //적 방향
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            swordman_moveSpeed = 2f;
        }
    }

    void UnitMove()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.localPosition += new Vector3(swordman_moveSpeed*Time.deltaTime, 0, 0);
            Debug.Log(transform.position);
        }
    }

    void UnitAnime()
    {
        //melee_State - Idle:0, Run:1, Attack:2
        //melee_Die - Alive:0, Die:1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetInteger("melee_State", 1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetInteger("melee_State", 2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetInteger("melee_State", 0);
        }

    }
}
