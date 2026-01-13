/************************************************************
* COPYRIGHT:  2026
* PROJECT: TheHollowCastle
* FILE NAME: PlayerContext.cs
* DESCRIPTION: Player Context keeps track of Player Components and Events.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/11 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class PlayerContext : MonoBehaviour
{
    public PlayerController controller;
    public PlayerMove move;
    public PlayerInventory inventory;
    public PlayerPickup pickup;
    public PlayerVitality vitality;
    public PlayerKeys keys;
    public PlayerWeaponSpawner weaponSpawner;
    public PlayerAnimationDriver animation;
}
