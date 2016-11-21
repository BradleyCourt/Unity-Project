using UnityEngine;
using System.Collections;

public class EnemyAnimationHandler : MonoBehaviour
{
    public Animator animCont;
    public EnemyHealth enemyHP;
	public EnemyAttack enemyAttack;
    public NavMeshAgent navCont;

	void Start()
	{
		enemyAttack.OnAttack += Attack;
		enemyHP.OnDeath += OnDeath;
	}

	void Update ()
    {
        float speed = (new Vector2 (navCont.velocity.x, navCont.velocity.z)).magnitude;
        animCont.SetFloat("movementSpeed", speed);
	}

	void Attack()
	{
		animCont.SetTrigger("attack");
	}

	void OnDeath()
	{
		animCont.SetBool("isDead", true);
		animCont.SetTrigger("justDied");
	}
}
