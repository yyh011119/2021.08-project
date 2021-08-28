using System.Collections;
using UnityEngine;

public class Unit7 : LivingEntity
{
    private float attackDelay = 0.75f;
    SpriteRenderer Bow0, Bow1, Bow2;
    Color color0, color1, color2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Bow0 = GameObject.Find("Bow0").GetComponent<SpriteRenderer>();
        Bow1 = GameObject.Find("Bow1").GetComponent<SpriteRenderer>();
        Bow2 = GameObject.Find("Bow2").GetComponent<SpriteRenderer>();
        color0 = Bow0.color;
        color1 = Bow1.color;
        color2 = Bow2.color;
        StartCoroutine(Run());
    }

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
            yield return StartCoroutine(BowFade());
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

    private IEnumerator BowFade()
    {
        float f = 1f;
        while (f > 0)
        {
            f -= 0.1f;
            Bow0.color = new Color(1, 1, 1, f);
            Bow1.color = new Color(1, 1, 1, 1-f);
            yield return new WaitForSeconds(0.05f * attackDelay / attackSpeed);
        }
        f = 1f;
        while (f > 0)
        {
            f -= 0.1f;
            Bow1.color = new Color(1, 1, 1, f);
            Bow2.color = new Color(1, 1, 1, 1 - f);
            yield return new WaitForSeconds(0.05f * attackDelay / attackSpeed);
        }
        Bow2.color = new Color(1, 1, 1, 0);
        Bow0.color = new Color(1, 1, 1, 1);
    }

}