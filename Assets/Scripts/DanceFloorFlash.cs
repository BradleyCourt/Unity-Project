using UnityEngine;
using System.Collections;

public class DanceFloorFlash : MonoBehaviour
{
    public MeshRenderer renderer;
    public float beatsPerMin = 100;
    private Material editMat;


	// Use this for initialization
	void Start ()
    {
        editMat = new Material(renderer.sharedMaterial);
        editMat.name = "bob";
        renderer.material = editMat;
        StartCoroutine(FloorChange());
	}

    IEnumerator FloorChange()
    {
        int dirReset = Random.Range(10, 25);
        int loops = 0;
        bool horScroll = true;
        while (true)
        {
            if (loops >= dirReset)
            {
                horScroll = !horScroll;
                dirReset = Random.Range(10, 25);
                loops = 0;
            }

            Vector2 offset = editMat.GetTextureOffset("_MainTex");
            offset += (horScroll) ? new Vector2(0.125f, 0) : new Vector2(0, 0.125f);
            editMat.SetTextureOffset("_MainTex", offset);
            yield return new WaitForSeconds(60/beatsPerMin);
            loops += 1;
        }
    }

}
