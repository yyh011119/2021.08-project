using UnityEngine;

public class Base : LivingEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log(currentHealth);
        if (currentHealth <= 0 & !isDie)
        {
            isDie = true;
            Destroy(gameObject, 0);
        }
    }

}