using System.Collections;
using UnityEngine;

public class Unit7 : LivingEntity
{
    private float attackDelay = 0.80f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            yield return new WaitForSeconds((attackDelay) / attackSpeed);
            if (!EnemyCheck()) break;
            Ranged_SingleAttack(55);
            yield return new WaitForSeconds((1 - attackDelay) / attackSpeed);
            if (!EnemyCheck()) break;
        }
        yield return StartCoroutine("Run");
    }

}