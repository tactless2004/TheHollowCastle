/************************************************************
* COPYRIGHT:  2026
* PROJECT: The Hollow Castle
* FILE NAME: PlayerSound.cs
* DESCRIPTION: Listens for events that should trigger the player character to make sound.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/13 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public class PlayerSound : MonoBehaviour
{
    private PlayerContext player;
    private AudioEmitter emitter;

    [Header("SFX")]
    [SerializeField] private SoundEvent hurtSFX;
    [SerializeField] private SoundEvent attackSFX;

    private void Awake()
    {
        if (!player && !TryGetComponent(out player))
        {
            Debug.LogError("PlayerSound could not find PlayerContext");
        }

        if (!emitter && !TryGetComponent(out emitter))
        {
            Debug.LogError("PlayerSound could not find AudioEmitter.");
        }
    }

    private void OnEnable()
    {
        player.vitality.OnDamaged += HandleDamaged;
        player.weaponSpawner.OnAttackPerformed += HandleAttack;
    }

    private void OnDisable()
    {
        player.vitality.OnDamaged -= HandleDamaged;
        player.weaponSpawner.OnAttackPerformed -= HandleAttack;
    }

    private void HandleDamaged(WeaponData attack)
    {
        emitter.Play(hurtSFX);
    }

    private void HandleAttack(WeaponData weapon)
    {
        emitter.Play(attackSFX);
        emitter.PlayOneShot(weapon.attackSoundEvent);
    }
}
