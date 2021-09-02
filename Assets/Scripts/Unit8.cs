using System.Collections;
using UnityEngine;

public class Unit8 : LivingEntity
{
    private float attackDelay = 0.14f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stunTime = 0.5f;
        StartCoroutine("Run");
    }

    protected override void Update()
    {
        base.Update();
        if (isDie)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Attack()
    {
        anim.SetInteger("state", 2);
        while (true)
        {
            yield return new WaitForSeconds(attackDelay / attackSpeed);
            if (!EnemyCheck()) break;
            Ranged_SingleAttack(500);
            yield return new WaitForSeconds((1 - attackDelay) / attackSpeed);
            if (!EnemyCheck()) break;
        }
        yield return StartCoroutine("Run");
    }

}