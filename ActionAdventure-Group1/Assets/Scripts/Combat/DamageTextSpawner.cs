/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: DamageTextSpawner.cs
* DESCRIPTION: Spawns UI elements visually showing damage dealt.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/13 | Leyton McKinney | Init
* 2025/11/20 | Leyton McKinney | Make enemy the transform parent of the instantiated text.
* 2025/11/20 | Leyton McKinney | Offset the instantiated text position.
*
************************************************************/
 
using UnityEngine;
 

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] Color resistColor = new Color(1.0f, 0.0f, 0.0f);
    [SerializeField] private float verticalTextOffset = 1.0f;

    public void Spawn(Transform spawnPosition, WeaponData weapon, bool resist, bool crit)
    {
        GameObject textInstance = Instantiate(
            textPrefab,
            spawnPosition.position + Vector3.up * verticalTextOffset,
            Quaternion.identity,
            spawnPosition // transform parent
        );
        
        // If resist, use resist color otherwise use white
        Color color = resist ? resistColor : new Color(1.0f, 1.0f, 1.0f);

        if(textInstance.TryGetComponent(out DamageText damageText))
        {
            damageText.Setup((int) Mathf.Floor(weapon.damage), color, crit);

            // Least janky way to do this, it just orients the damage text in the same way the camera is oriented,
            // previous we did some weird rotation business that did not behave as expected.
            damageText.transform.forward = Camera.main.transform.forward;
        } else
        {
            Destroy(textInstance);
        }
    }
}
