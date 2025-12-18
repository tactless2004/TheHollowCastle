/************************************************************
* COPYRIGHT:  Year
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: PlayerCombat.cs
* DESCRIPTION: Describes the Combat Behaviors of the player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | Switch from (Range, Melee) scheme to (Slot1, Slot2 Scheme), change cooldown to be weapon specific.
* 2025/11/12 | Leyton McKinney | Change Attack() calls to use targetTag field.
* 2025/12/17 | Leyton McKinney | Remove Debug code because it is outdated
*
*
************************************************************/

using System;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform rangedAttackOrigin;
    [SerializeField] private Transform meleeAttackOrigin;
    [SerializeField] private ScriptableObject Slot1_WeaponData;
    [SerializeField] private ScriptableObject Slot2_WeaponData;

    private PlayerMove playerMove;
    public Weapon Slot1_Weapon;
    public Weapon Slot2_Weapon;
#if UNITY_EDITOR
    [Header("DEBUG")]
    [SerializeField] private bool showSlot1Range;
    [SerializeField] private bool showSlot2Range;
#endif

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        Slot1_Weapon = new Weapon(Slot1_WeaponData as WeaponData);
        Slot2_Weapon = new Weapon(Slot2_WeaponData as WeaponData);
    }

    public void SetWeapon(int slot, WeaponData weapon)
    {
        if (slot == 1) Slot1_Weapon = new Weapon(weapon);
        else Slot2_Weapon = new Weapon(weapon);
    }

    public void Slot1_Attack()
    {
        // We need to check if it is ranged or melee, to decided where the "attack origin" is
        if (Slot1_Weapon.getWeaponData().category == WeaponCategory.Melee)
            Slot1_Weapon.Attack(meleeAttackOrigin.position, playerMove.facing, gameObject, "Enemy");
        else
            Slot1_Weapon.Attack(rangedAttackOrigin.position, playerMove.facing, gameObject, "Enemy");
    }

    public void Slot2_Attack()
    {
        // We need to check if it is ranged or melee, to decided where the "attack origin" is
        if (Slot2_Weapon.getWeaponData().category == WeaponCategory.Melee)
            Slot2_Weapon.Attack(meleeAttackOrigin.position, playerMove.facing, gameObject, "Enemy");
        else
            Slot2_Weapon.Attack(rangedAttackOrigin.position, playerMove.facing, gameObject, "Enemy");
    }
}
