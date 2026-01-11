/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: Chest.cs
* DESCRIPTION: Logic for Chest Mechanic.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/08 | Leyton McKinney | Init
*
************************************************************/

using System.Collections.Generic;
using UnityEngine;
 

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator chestAnim;
    [SerializeField] private GameObject pickupWeapon;
    [SerializeField] private bool open = false;
    [SerializeField] ScriptableObject chestRollTableSO;
    [SerializeField] Transform modelTransform;
    [SerializeField] Collider triggerCollider;

    private ChestRollTable chestRollTable;

    private void Awake() {
        chestRollTable = chestRollTableSO as ChestRollTable;
        triggerCollider = GetComponent<BoxCollider>();
        RollWeapon();
    }

    private void RollWeapon() {
        float roll = Random.value; // [0.0f, 1.0f];
        float cumulative = 0.0f;

        cumulative += chestRollTable.CommonProbability;
        if (roll < cumulative) {
            pickupWeapon = GetRandomWeaponFromList(chestRollTable.CommonWeapons);
            return;
        }   

        cumulative += chestRollTable.UncommonProbability;
        if (roll < cumulative) {
            pickupWeapon = GetRandomWeaponFromList(chestRollTable.UncommonWeapons);
            return;
        }

        cumulative += chestRollTable.RareProbability;
        if (roll < cumulative) {
            pickupWeapon = GetRandomWeaponFromList(chestRollTable.RareWeapons);
            return;
        }

        pickupWeapon = GetRandomWeaponFromList(chestRollTable.LegendaryWeapons);
    }

    private GameObject GetRandomWeaponFromList(List<GameObject> list) {
        if (list == null || list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }

    // Called by an event in the chest animation.
    public void SpawnWeapon() {
        GameObject pickupWeaponInstance = Instantiate(
            pickupWeapon, //obj
            modelTransform.position + new Vector3(0.0f, 0.5f, 0.0f),//position
            Quaternion.Euler(-90.0f, 0.0f, 0.0f) //rotation
        );
        triggerCollider.enabled = false;
        pickupWeaponInstance.SetActive(true);
    }
    
    public bool isOpened() {
        return open;
    }

    public void Open() {
        if (open) return;
        open = true;
        chestAnim.Play("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }


}//end Chest
