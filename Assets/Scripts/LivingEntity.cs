using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    protected Animator anim;
    protected GameObject detect_Collider;
    protected Rigidbody2D rigid;

    public float health;
    protected float currentHealth;
    public float damage;
    protected float currentDamage;
    public float defense;
    protected float currentDefense;
    public float pierce;
    protected float currentPierce;
    public float attackSpeed;
    protected float currentAttackSpeed;
    public float moveSpeed;
    protected float currentMoveSpeed;
    public int dropPoint;
    protected float stunTime = 0;
    protected float revival = 0;

    protected bool isDie = false;
    protected bool pointGiven = false;

    public GameObject hp; // hp바 구현용
    public GameObject canvas;
    protected float h = 50.0f;
    RectTransform hpBar; // hp바

    private Collider2D target;
    private float shortest;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        detect_Collider = transform.Find("Detect").gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        canvas = GameObject.Find("Canvas");
        Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
        hpBar = Instantiate(hp, canvas.transform).GetComponent<RectTransform>();
        hpBar.position = hpBarPosition;

        Determine_Stats();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
        //hpBarPosition.y += h;
        //if(isDie==false)
        //{
        //    hpBar.position = hpBarPosition;
        //    hpBar.transform.GetChild(1).localScale = new Vector3(currentHealth / health, 1, 1);
        //}

        HpbarCreate(2);

        DieCheck();
        AnimSpeedCheck();
    }

    protected void Determine_Stats()
    {
        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;
        currentDefense = defense;
        currentPierce = pierce;

        //아군 방향 속도
        if (tag == "Enemy")
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            currentMoveSpeed = -moveSpeed;
        }

        //적 방향 속도
        else if (tag == "Ally")
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            currentMoveSpeed = moveSpeed;
        }

    }

    protected void AnimSpeedCheck()
    {
        anim.SetFloat("run", currentMoveSpeed);
        anim.SetFloat("attack", currentAttackSpeed);
    }

    protected void DieCheck()
    {
        if (currentHealth <= 0 & !isDie)
        {
            if(revival>0)
            {
                Revive();
            }
            else Die();
        }
    }

    protected void Die()
    {
        isDie = true;
        anim.SetBool("die", true);
        if (dropPoint != 0) GivePoint(dropPoint);
        Destroy(hpBar.gameObject);
        Destroy(gameObject, 2);
    }


    protected void Revive()
    {
        StopAllCoroutines();
        currentHealth = health;
        revival -= 1;
        foreach (ColorChange colorChange in this.GetComponentsInChildren<ColorChange>())
        {
            colorChange.RevivalColor();
        }
        StartCoroutine(Stun(0, 0.5f));
    }

    protected bool EnemyCheck() //사거리 내 적 존재여부 반환
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.Find("Detect").position, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false) return true;
            else if (this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) return true;
        }
        return false;
    }

    //공격 종류들
    protected void Melee_SingleAttack()
    {
        target = null;
        shortest = 999999;
        float distance;
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            //거리가 가장 가까운 살아있는 타겟 찾기
            if ((this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) || (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false))
            {
                distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < shortest)
                {
                    target = collider;
                    shortest = distance;
                }
            }
        }
        if (target != null)
        {
            target.GetComponent<LivingEntity>().TakeDamage(currentDamage, currentPierce, 0);
            target.GetComponent<LivingEntity>().TakeDebuff(stunTime, 0);
        }
    }

    protected void Melee_MultiAttack()
    {
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        target = null;
        shortest = 999999;
        float distance;
        foreach (Collider2D collider in collider2Ds)
        {
            if ((this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) || (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false))
            {
                distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < shortest)
                {
                    target = collider;
                    shortest = distance;
                }
            }
        }
        if (target != null)
        {
            target.GetComponent<LivingEntity>().TakeDamage(currentDamage, currentPierce, 0);
            target.GetComponent<LivingEntity>().TakeDebuff(stunTime, 0);
        }
        foreach (Collider2D collider in collider2Ds)
        {
            if ((this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) || (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false))
            {
                collider.GetComponent<LivingEntity>().TakeDamage(currentDamage * 0.5f, currentPierce * 0.5f, 0);
                target.GetComponent<LivingEntity>().TakeDebuff(stunTime, 0);
            }
        }
    }

    protected void Ranged_SingleAttack(float relativeSpeed)
    {
        target = null;
        shortest = 999999;
        float distance;
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if ((this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) || (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false))
            {
                distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < shortest)
                {
                    target = collider;
                    shortest = distance;
                }
            }
        }
        if (target != null)
        {
            target.GetComponent<LivingEntity>().TakeDamage(currentDamage, currentPierce, shortest / relativeSpeed);
            target.GetComponent<LivingEntity>().TakeDebuff(stunTime, shortest / relativeSpeed);
        }
    }

    protected void Witch_attack()
    {
        float lowestHealth = 100000;
        Vector2 attackSpot = transform.Find("Detect").position;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSpot, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if ((this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) || (this.tag == "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false))
            {
                if(lowestHealth > collider.GetComponent<LivingEntity>().currentHealth)
                {
                    target = collider;
                    lowestHealth = collider.GetComponent<LivingEntity>().currentHealth;
                }
            }
        }
        if (target != null)
        {
            target.GetComponent<LivingEntity>().TakeDamage(currentDamage, currentPierce, 0);
            target.GetComponent<LivingEntity>().TakeDebuff(stunTime, 0);
        }
    }

    protected IEnumerator Run()
    {
        anim.SetInteger("state", 1);
        while (!EnemyCheck())
        {
            transform.localPosition += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        yield return StartCoroutine("Attack");
    }
    
    //데미지 및 상태이상
    public void TakeDamage(float damage, float pierce, float delay)
    {
        float totalMultiplier = (100 - currentDefense + pierce) / 100;
        if (totalMultiplier > 1) totalMultiplier = 1.1f;
        float totalDamage = damage * totalMultiplier;
        if (totalDamage <= 0) totalDamage = 1;
        StartCoroutine(GiveDamage(totalDamage, delay));
    }

    public IEnumerator GiveDamage(float totalDamage, float delay)
    {
        yield return new WaitForSeconds(delay);
        currentHealth = currentHealth - totalDamage;
        if (currentHealth < 0) currentHealth = 0;
    }

    protected void TakeDebuff(float stunTime, float delay)
    {
        if (stunTime != 0 && anim.GetInteger("state") != 0) StartCoroutine(Stun(stunTime, delay));
    }

    protected IEnumerator Stun(float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        StopCoroutine("Run");
        StopCoroutine("Attack");
        anim.SetInteger("state", 0);
        yield return new WaitForSeconds(time);
        StartCoroutine("Run");
    }

    public void GivePoint(int dropPoint)
    {
        GameObject.Find("AllyBase").GetComponent<PointControl>().point += dropPoint;
        pointGiven = true;
    }

    protected void HpbarCreate(int flag)
    {
        if (flag == 1)
        {
            canvas = GameObject.Find("Canvas");
            Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
            hpBar = Instantiate(hp, canvas.transform).GetComponent<RectTransform>();
            hpBar.position = hpBarPosition;
            hpBar.localScale = new Vector3(2, 1, 1);
            h = 100.0f;
        }

        if (flag == 2)
        {
            Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
            hpBarPosition.y += h;
            if (isDie == false)
            {
                hpBar.position = hpBarPosition;
                hpBar.transform.GetChild(1).localScale = new Vector3(currentHealth / health, 1, 1);
            }
        }

    }



}
