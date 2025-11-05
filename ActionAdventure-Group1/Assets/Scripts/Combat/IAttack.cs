/************************************************************
* COPYRIGHT:  2025
* PROJECT: ActionAdventureGameNameTBA
* FILE NAME: IAttack.cs
* DESCRIPTION: Interface for attacks, agnostic of attack type or damage type.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
*
************************************************************/
 
using UnityEngine;
 

public interface IAttack
{
    void Exectute(Vector3 origin, Vector3 direction, GameObject source);
}
