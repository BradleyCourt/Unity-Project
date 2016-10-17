using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    private NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //GetComponent<NavMeshAgent>().destination = player.transform.position;
        agent.SetDestination(player.transform.position);
        
    }
}
