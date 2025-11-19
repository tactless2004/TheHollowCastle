/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: WallTransparency.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/17 | Peyton Lenard | Created class
* 2025/11/18 | Peyton Lenard | Fixed Trigger methods and transform position logic
*
************************************************************/
 
using UnityEngine;
 

public class WallTransparency : MonoBehaviour
{
    void OnTriggerEnter(Collider outsideWall)
    {
        if(outsideWall.CompareTag("MoveableWall"))
        {
            //Move outer wall below backdrop
            outsideWall.transform.position = outsideWall.transform.position + new Vector3(0, -10, 0);
            
            //Debug.Log("Wall entered player area.");
        }
    }
    void OnTriggerExit(Collider outsideWall)
    {
        if(outsideWall.CompareTag("MoveableWall"))
        {
            //Move outer wall back to original position
            outsideWall.transform.position = outsideWall.transform.position - new Vector3(0, -10, 0);
            
            //Debug.Log("Wall left player area.");
        }
    }
 
 
    /// <summary>
    /// A custom method example.
    /// </summary>
    /// <param name="exampleParameter"> A parameter that demonstrates passing data to the method.
    ///</param>
    private void CustomMethod(int exampleParameter)
    {
        
        
    }//end CustomMethod(int)
 
 
}//end WallTransparency
