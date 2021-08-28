using System.Collections;
using UnityEngine;

public class Enemy1 : LivingEntity
{
    private float attackDelay = 0.55f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Run());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isDie) StopAllCoroutines();
    }

    private IEnumerator Attack()
    {
        anim.SetInteger("state", 2);
        while (true)
        {
            yield return new WaitForSeconds(attackDelay / attackSpeed);
            if (!EnemyCheck()) break;
            Melee_SingleAttack();
            yield return new WaitForSeconds((1 - attackDelay) / attackSpeed);
            if (!EnemyCheck()) break;
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

}
