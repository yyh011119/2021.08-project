using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    protected Animator anim;
    protected GameObject detect_Collider;
    protected Rigidbody2D rigid;

    protected float attackTime;
    protected float dieTime;
    protected float runTime;

    public float health;
    protected float currentHealth;
    public float moveSpeed;
    protected float currentMoveSpeed;
    public float attackSpeed;
    protected float currentAttackSpeed;
    public float damage;
    protected float currentDamage;
    protected float attackCooldown;

    protected int unit_State = 1;
    protected bool isDie = false;
    protected bool pointGiven = false;

    public GameObject hp; // hp바 구현용
    public GameObject canvas;
    protected float h = 50.0f;

    RectTransform hpBar; // hp바

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

        Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
        hpBarPosition.y += h;
        if(isDie==false)
        {
            hpBar.position = hpBarPosition;
            hpBar.transform.GetChild(1).localScale = new Vector3(currentHealth / health, 1, 1);
        }

        DieCheck();
        AnimSpeedCheck();
    }

    protected void Determine_Stats()
    {
        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;

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

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }

    protected void DieCheck()
    {
        if (currentHealth <= 0 & !isDie)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDie = true;
        anim.SetBool("die", true);
        Destroy(hpBar.gameObject);
        Destroy(gameObject, 2);
    }

    protected void CooldownCheck(float attackDelay)
    {
        if (unit_State == 2)
        {
            if (attackCooldown <= 0)
            {
                attackCooldown = 1 / currentAttackSpeed;
            }

            attackCooldown -= Time.deltaTime;
        }
        else attackCooldown =  attackDelay / currentAttackSpeed;
    }

    protected void EnemyCheck(float attackDelay)
    {
        int sum = 0;

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.Find("Detect").position, detect_Collider.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (this.tag== "Enemy" && collider.tag == "Ally" && collider.GetComponent<LivingEntity>().isDie == false) sum++;
            else if (this.tag == "Ally" && collider.tag == "Enemy" && collider.GetComponent<LivingEntity>().isDie == false) sum++;
        }

        if (sum != 0) unit_State = 2;
        else if (attackCooldown <= attackDelay / currentAttackSpeed) unit_State = 1;
    }

    public void givePoint(int givenPoint)
    {
        GameObject.Find("AllyBase").GetComponent<PointControl>().point += givenPoint;
        pointGiven = true;
    }

    protected void declare(int flag)
    {
        if(flag==1)
        {
            canvas = GameObject.Find("Canvas");
            Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(transform.position);
            hpBar = Instantiate(hp, canvas.transform).GetComponent<RectTransform>();
            hpBar.position = hpBarPosition;
            hpBar.localScale = new Vector3(2, 1, 1);
            h = 100.0f;
        }

        if(flag==2)
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
