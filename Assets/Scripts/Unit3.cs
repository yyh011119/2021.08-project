using System.Collections;
using UnityEngine;

public class Unit3 : LivingEntity
{
    private float attackDelay = 0.55f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Run());
        if (isDie) StopAllCoroutines();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator Attack()
    {
        anim.SetInteger("state", 2);
        while (true)
        {
            yield return new WaitForSeconds(attackDelay / attackSpeed);
            Melee_Attack();
            yield return new WaitForSeconds((1 - attackDelay) / attackSpeed);
            if (EnemyCheck() == false) break;
        }
        yield return StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        anim.SetInteger("state", 1);
        while (!EnemyCheck())
        {
            transform.localPosition += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        yield return StartCoroutine(Attack());
    }

    void Melee_Attack()
    {
        int sum = 0;

        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy") sum++;
        }

        if (sum >= 3)
        {
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Enemy")
                {
                    collider.GetComponent<LivingEntity>().TakeDamage(currentDamage * 0.5f);
                }
            }
        }
        else if (sum >= 1)
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
