/************************************************************
* COPYRIGHT:  2025
* PROJECT: The Hollow Castle
* FILE NAME: ChestRollTable.cs
* DESCRIPTION: ScriptableObject for rolling chest weapons, should be different level-to-level or chest-type-to-chest-type.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/08 | Leyton McKinney | Init
*
************************************************************/

using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "NewChestRollTable", menuName = "Chest/Chest Roll Table")]
public class ChestRollTable : ScriptableObject
{
    [Header("Probabilities (Sum of probs should = 1.0)")]
    public float CommonProbability;
    public float UncommonProbability;
    public float RareProbability;
    public float LegendaryProbability;

    [Header("Common")]
    public List<GameObject> CommonWeapons;

    [Header("Uncommon")]
    public List<GameObject> UncommonWeapons;

    [Header("Rare")]
    public List<GameObject> RareWeapons;

    [Header("Legendary")]
    public List<GameObject> LegendaryWeapons;
}
