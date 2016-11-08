using UnityEngine;
using System.Collections;

public class AICosting : MonoBehaviour
{
    float cost;
    int areaIndex;
    public string areaName;

	// Use this for initialization
	void Start ()
    {
        areaIndex = NavMesh.GetAreaFromName(areaName);
        cost = NavMesh.GetAreaCost(areaIndex);        
    }
	
	// Update is called once per frame
	void Update ()
    {
        NavMesh.SetAreaCost(areaIndex, cost);
       // Debug.Log(NavMesh.GetAreaCost(areaIndex));
        
        //Debug.Log(transform.name);
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering");
        Debug.Log(other.tag);        
        
        if (other.tag == "Enemy")
        {
            Debug.Log("Adding Cost");
            cost += 1;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy Left");
            cost--;
        }
    }
}
