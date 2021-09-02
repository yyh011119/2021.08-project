using System.Collections;
using UnityEngine;

public class Unit2 : LivingEntity
{
    private float attackDelay = 0.25f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine("Run");
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
            Melee_MultiAttack();
            yield return new WaitForSeconds((1 - attackDelay) / attackSpeed);
            if (!EnemyCheck()) break;
            Debug.Log("Attacking!");
        }
        yield return StartCoroutine("Run");
    }


}



