using UnityEngine;

public class Swordman_behavior : MonoBehaviour
{
    private Animator anim;
    protected GameObject detect_Collider;
    protected Rigidbody2D rigid;
    public Transform pos;
    public Vector2 boxSize;

    private float attackTime;
    private float dieTime;
    private float runTime;

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
        UpdateAnimClipTimes();
        attackCooldown = 0.7f * attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attackCooldown);
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
        if (collision.tag == "Unit")
        {
            Debug.Log("Collision!");
            unit_State = 2;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        unit_State = 1;
    }
    
    private void Attack()
    {
        anim.SetInteger("anime_State", 2);
        if (attackCooldown <= 0)
        {
            Melee_Attack();
            attackCooldown = attackTime;
        }
        attackCooldown -= Time.deltaTime;
    }

    private void Run()
    {
        attackCooldown = 0.5f * currentAttackSpeed;
        transform.localPosition += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
        anim.SetInteger("anime_State", 1);
    }

    private void Stay()
    {
        attackCooldown = 0.5f * currentAttackSpeed;
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
        if(currentHealth<=0&!isDie)
        {
            Die();
        }
    }

    void Die()
    {
        isDie = true;
        anim.SetBool("Die", true);
        Destroy(gameObject, 2);
    }

    void Melee_Attack()
    {
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach(Collider2D collider in collider2Ds)
        {
            if(collider.tag == "Unit")
            {
                Debug.Log(currentHealth);
                collider.GetComponent<Swordman_behavior>().TakeDamage(currentDamage);
                return;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth = currentHealth - damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.Find("Detect").position, detect_Collider.GetComponent<BoxCollider2D>().size);
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Melee_attack":
                    attackTime = clip.length;
                    break;
                case "Melee_die":
                    dieTime = clip.length;
                    break;
                case "Melee_run":
                    runTime = clip.length;
                    break;
            }
        }
    }

}

