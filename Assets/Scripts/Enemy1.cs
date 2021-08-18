using UnityEngine;

public class Enemy1 : LivingEntity
{

    private float attackCooldown;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

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

        CooldownCheck();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ally" && collision.GetComponent<LivingEntity>().isDie == false)
        {
            Debug.Log("Collision!");
            unit_State = 2;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        int sum = 0;

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.Find("Detect").position, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Ally") sum++;
        }

        if (sum != 0) unit_State = 2;
        else unit_State = 1;
    }

    private void Attack()
    {
        anim.SetInteger("anime_State", 2);
        if (attackCooldown <= 0)
        {
            Melee_Attack();
        }
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

    void Melee_Attack()
    {
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Ally")
            {
                collider.GetComponent<LivingEntity>().TakeDamage(currentDamage);
                Debug.Log(currentHealth);
                return;
            }
        }
    }

    public void CooldownCheck()
    {
        if (unit_State == 2)
        {
            if (attackCooldown <= 0)
            {
                attackCooldown = attackTime * currentAttackSpeed / 0.5f;
            }

            attackCooldown -= Time.deltaTime;
        }
        else attackCooldown = 0.6f * attackTime * currentAttackSpeed / 0.5f;
    }

}
