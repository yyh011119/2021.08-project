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

    protected int unit_State = 1;
    public bool isDie = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        detect_Collider = transform.Find("Detect").gameObject;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        Determine_Stats();
        UpdateAnimClipTimes();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DieCheck();
    }

    void Determine_Stats()
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

    void Die()
    {
        isDie = true;
        anim.SetBool("Die", true);
        Destroy(gameObject, 2);
    }
}
