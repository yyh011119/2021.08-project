using UnityEngine;

public class Enemy1 : LivingEntity
{
    private float attackDelay = 0.6f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        CooldownCheck(attackDelay);
        EnemyCheck(attackDelay);

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

    private void Attack()
    {
        anim.SetInteger("state", 2);
        if (attackCooldown <= 0)
        {
            Melee_Attack();
        }
    }

    private void Run()
    {
        transform.localPosition += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
        anim.SetInteger("state", 1);
    }

    private void Stay()
    {
        anim.SetInteger("state", 0);
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
                return;
            }
        }
    }

}
