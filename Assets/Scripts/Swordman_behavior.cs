using UnityEngine;

public class Swordman_behavior : MonoBehaviour
{
    public float init_Health;
    private float health;
    public float init_Speed;
    private float speed;
    public float init_Damage;
    private float damage;
    public bool ally;
    private Animator anim;
    public Transform pos;
    public Vector2 boxSize;
    protected GameObject detect_Collider;
    private int unit_State = 1;

    // Start is called before the first frame update
    void Start()
    {
        detect_Collider = transform.Find("Detect").gameObject;
        anim = GetComponent<Animator>();
        Determine_Stats();
    }

    // Update is called once per frame
    void Update()
    {
        if (unit_State == 2)
        {
            Attack();
        }

        else if (unit_State == 1)
        {
            Run();
        }

        else if (unit_State == 0)
        {
            Stay();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision!");
        unit_State = 2;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        unit_State = 1;
    }
    
    private void Attack()
    {
        anim.SetInteger("anime_State", 2);
        Melee_Attack();
    }

    private void Run()
    {
        transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
        anim.SetInteger("anime_State", 1);
    }

    private void Stay()
    {
        anim.SetInteger("anime_State", 0);
    }

    void Determine_Stats()
    {
        health = init_Health;
        damage = init_Damage;

        //아군 방향 속도
        if (ally == true)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            speed = -init_Speed;
        }

        //적 방향 속도
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            speed = init_Speed;
        }
    }

    void Melee_Attack()
    {

    }
}
