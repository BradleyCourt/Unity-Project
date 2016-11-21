using UnityEngine;
using System.Collections;

public class EnemyAnimationHandler : MonoBehaviour
{
    public Animator animCont;
    public EnemyHealth enemyHP;
    public NavMeshAgent navCont;

	void Update ()
    {
        float speed = (new Vector2 (navCont.velocity.x, navCont.velocity.z)).magnitude;
        animCont.SetFloat("movementSpeed", speed);
	}
}
