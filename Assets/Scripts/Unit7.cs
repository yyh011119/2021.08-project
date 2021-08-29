using System.Collections;
using UnityEngine;

public class Unit7 : LivingEntity
{
    private float attackDelay = 0.63f;
    GameObject projectile;
    private SpriteRenderer bow0, bow1, bow2, arrow0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bow0 = transform.GetChild(0).GetChild(1).Find("Bow0").gameObject.GetComponent<SpriteRenderer>();
        bow1 = transform.GetChild(0).GetChild(1).Find("Bow1").gameObject.GetComponent<SpriteRenderer>();
        bow2 = transform.GetChild(0).GetChild(1).Find("Bow2").gameObject.GetComponent<SpriteRenderer>();
        arrow0 = transform.GetChild(0).GetChild(1).Find("Arrow").gameObject.GetComponent<SpriteRenderer>();
        projectile = transform.GetChild(0).GetChild(1).Find("Arrow").gameObject;
        StartCoroutine(Run());
    }

    protected override void Update()
    {
        base.Update();
        if (isDie)
        {
            bow2.color = new Color(1, 1, 1, 0);
            bow0.color = new Color(1, 1, 1, 1);
            arrow0.color = new Color(1, 1, 1, 0);
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
            Ranged_SingleAttack();
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