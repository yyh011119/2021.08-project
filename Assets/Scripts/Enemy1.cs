﻿using System.Collections;
using UnityEngine;

public class Enemy1 : LivingEntity
{
    private float attackDelay = 0.55f;
    public int dropPoint;

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
