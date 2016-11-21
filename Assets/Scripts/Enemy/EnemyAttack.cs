using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    [Tooltip("What the enemy should attack")]
    public GameObject target;

    [Header("Enemy Attack Stats")]
    [Tooltip("Time it takes before damage is applied")]
    public float attackTimer;
    [Tooltip("Time before agent can attack again")]
    public float attackCooldown;
    [Tooltip("Distance the agent can attack up to")]
    public float attackRange;

    [Tooltip("Amount of health to take away")]
    public int attackDamage;

	public delegate void AttackEvent();
	public event AttackEvent OnAttack; 

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


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
        if (distance < attackRange)
        {
			OnAttack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().AffectHealth(-attackDamage);
        }
    }
}
