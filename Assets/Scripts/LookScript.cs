using UnityEngine;
using System.Collections;

public class LookScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 lookVec = new Vector3(-Input.GetAxis("LookVertical"), 0, Input.GetAxis("LookHorizontal"));
      //  Debug.Log("Magnitude " + lookVec);
        if (lookVec.magnitude >0.1)
        {
            Quaternion charRotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), lookVec);
            transform.rotation = charRotation;
        }

        Debug.Log(lookVec);
	}
}
