using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : Health
{
    public GameObject target;

    public float attackTimer;
    public float attackCooldown;
    public float attackRange;
    public int attackDamage;
    

    public override void Death()
    {

    }

    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
            attackTimer = 0;

        if (attackTimer == 0)
        {
            EnemyAttack();
            attackTimer = attackCooldown;
        }
    }


    private void EnemyAttack()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        //    Vector3 dir = (target.transform.position - transform.position).normalized;

        //    float direction = Vector3.Dot(dir, transform.forward);

        if (distance < attackRange)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().AffectHealth(-attackDamage);
}
    }
}