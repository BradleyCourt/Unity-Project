using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour
{
    public Animator animCont;
    public PlayerHealth playerHP;

    void Start()
    {

    }

	void Update ()
    {
	    if (playerHP.isDead)
        {
            animCont.SetBool("isDead", true);
        }
	}
}
