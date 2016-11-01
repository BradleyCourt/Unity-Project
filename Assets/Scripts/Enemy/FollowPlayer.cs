using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    Health health;
    private NavMeshAgent agent;
    public GameObject player;
    public float attackRange = 0f;
    float Distance;
    public float Defaultspeed = 3.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        attack();
        Distance = Vector3.Distance(transform.position, player.transform.position);
    }
    public void attack()
    {       
        if (Distance <= attackRange)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = Defaultspeed;
            agent.SetDestination(player.transform.position);
        }
    }
}
