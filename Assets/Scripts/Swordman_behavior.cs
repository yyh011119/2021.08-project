using UnityEngine;

public class Swordman_behavior : MonoBehaviour
{
    private Animator anim;
    protected GameObject detect_Collider;
    protected Rigidbody2D rigid;
    public Transform pos;
    public Vector2 boxSize;

    public float health;
    private float currentHealth;
    public float moveSpeed;
    private float currentMoveSpeed;
    public float attackSpeed;
    private float currentAttackSpeed;
    public float damage;
    private float currentDamage;
    public bool ally;

    
    private int unit_State = 1;
    private float attackCooldown;

    bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        detect_Collider = transform.Find("Detect").gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        Determine_Stats();
    }

    // Update is called once per frame
    void Update()
    {
        DieCheck();
        if (isDie == false)
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
        transform.localPosition += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
        anim.SetInteger("anime_State", 1);
    }

    private void Stay()
    {
        anim.SetInteger("anime_State", 0);
    }

    void Determine_Stats()
    {
        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;

        //아군 방향 속도
        if (ally == true)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            currentMoveSpeed = -moveSpeed;
        }

        //적 방향 속도
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            currentMoveSpeed = moveSpeed;
        }
    }

    void DieCheck()
    {
        if(currentHealth<=0)
        {
            if(!isDie)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDie = true;

        rigid.velocity = Vector2.zero;

        anim.SetBool("Die", true);
        Destroy(gameObject, 2);

    }

    void Melee_Attack()
    {
        Debug.Log(currentHealth);
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapPointAll(attackSpot);
        foreach(Collider2D collider in collider2Ds)
        {
            if(collider.tag == "Unit")
            {
                collider.GetComponent<Swordman_behavior>().TakeDamage(currentDamage);
            }
        }


    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }
}

