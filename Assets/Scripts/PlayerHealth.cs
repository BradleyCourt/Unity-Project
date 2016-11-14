using UnityEngine;
using System;
using System.Collections;

public class PlayerHealth : Health
{
    TopDownController topDownController;
    CombatController combatController;
    LookScript lookScript;

    void Start()
    {
        topDownController = GetComponent<TopDownController>();
        combatController = GetComponent<CombatController>();
        lookScript = GetComponentInChildren<LookScript>();
    }


    public override void Death()
    {
        combatController.enabled = false;
        topDownController.enabled = false;
        lookScript.enabled = false;
        Debug.Log("You are dead - Game Over");
    }

}
