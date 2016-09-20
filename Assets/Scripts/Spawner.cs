using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
    private Transform[] pool;
	// Use this for initialization
	void Start ()
    {
        pool = new Transform[100];
        for (int i = 0; i < pool.Length; i++)
        {
            Transform thing = GameObject.Instantiate<Transform>(prefab);
            pool[i] = thing;
            thing.gameObject.SetActive(false);
            thing.hideFlags = HideFlags.HideInHierarchy;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButtonDown("Jump"))
        {
            Transform thing = null;
            for (int i = 0; i < pool.Length; i++)
            {
                if(!pool[i].gameObject.activeSelf)
                {
                    thing = pool[i];
                    thing.gameObject.SetActive(true);
                    break;
                }
            }
            if (thing !=null)
            {
                
                thing.position = transform.position;
                thing.rotation = transform.rotation;
                thing.GetComponent<Rigidbody>().velocity = transform.up * 100;
            }
              
        }
	}
}
