using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnemyHealth : Health
{
    public override void Death()
    {

    }
    //private void EnemyAttack()
    //{
    //    float distance = Vector3.Distance(target.transform.position, transform.position);

    //    Vector3 dir = (target.transform.position - transform.position).normalized;

    //    float direction = Vector3.Dot(dir, transform.forward);

    //    if (distance < 2.5f)
    //    {
    //        GameObject.FindGameObjectWithTag("Player").AffectHealth();
    //    }
    //}
}