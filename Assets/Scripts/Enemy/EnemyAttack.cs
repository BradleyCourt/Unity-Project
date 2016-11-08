using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public GameObject target;

    public float attackTimer;
    public float attackCooldown;
    public float attackRange;
    public int attackDamage;


    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
            attackTimer = 0;

        if (attackTimer == 0)
        {
            EnemyAttacking();
            attackTimer = attackCooldown;
        }
    }

    private void EnemyAttacking()
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
