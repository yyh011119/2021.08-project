using UnityEngine;

public class Unit2 : LivingEntity
{
    private float attackDelay = 0.27f;

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
            Spear_Attack();
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

    void Spear_Attack()
    {
        int sum = 0;

        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy") sum++;
        }

        if(sum>=3)
        {
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Enemy")
                {
                    collider.GetComponent<LivingEntity>().TakeDamage(currentDamage * 0.25f);
                }
            }
        }
        else if(sum>=1)
        {
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Enemy")
                {
                    collider.GetComponent<LivingEntity>().TakeDamage(currentDamage);
                    return;
                }
            }
        }

    }

    /*
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
    */

}



