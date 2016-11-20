using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator animCont;
    public PlayerHealth playerHP;
	public CombatController combatCont;
	public TopDownController playerCont;

	int currentWeapon = 0;

    void Start()
    {
		playerHP.OnDeath += OnDeath;
		combatCont.OnWeaponSwitch += OnWeaponSwitch;
		currentWeapon = combatCont.currentWeapon;

		//Bad code but no option
		if (currentWeapon == 0)
		{
			animCont.SetTrigger("typeRifle");
		}
		if (currentWeapon == 1)
		{
			animCont.SetTrigger("typeHandgun");
		}
	}

	void OnDestory()
	{
		playerHP.OnDeath -= OnDeath;
		combatCont.OnWeaponSwitch -= OnWeaponSwitch;
	}

	void Update ()
    {
		float moveSpeed = new Vector2(playerCont.moveDirection.x, playerCont.moveDirection.z).magnitude;
		animCont.SetFloat("movementSpeed", moveSpeed);
	}

	void OnWeaponSwitch(int weaponID)
	{
		if (weaponID == currentWeapon) { return; } else { currentWeapon = weaponID; }

		if (weaponID == 0)
		{
			animCont.SetTrigger("typeRifle");
		}
		if (weaponID == 1)
		{
			animCont.SetTrigger("typeHandgun");
		}
	}

	void OnDeath()
	{
		if (playerHP.isDead)
		{
			animCont.SetBool("isDead", true);
			animCont.SetTrigger("justDied");
		}
	}
}
