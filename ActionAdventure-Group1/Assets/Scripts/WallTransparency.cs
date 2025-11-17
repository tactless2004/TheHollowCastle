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
*
*
************************************************************/
 
using UnityEngine;
 

public class WallTransparency : MonoBehaviour
{

    // Awake is called once on initialization (before Start)
    private void Awake()
    {
        
        
    } //end Awake()
 
 
    // Start is called once before the first Update
    private void Start()
    {
        
        
    } //end Start()
 
 
    // Update is called once per frame
    private void Update()
    {
        
        
    } //end Update()

    void OnTriggerEnter(Collider transparentWall)
    {
        if(tag == "MoveableWall")
        {
            if(TryGetComponent<Transform>(out transparentWall))
            {
                Debug.Log("Wall position moved.");
                transparentWall.position = transparentWall.position + new Vector3(0, 5, 0);
            } else
            {
                Debug.Log("Wall not found.");
            }
        }
    }
    void OnTriggerExit(Collider transparentWall)
    {
        if(tag == "MoveableWall")
        {
            if(TryGetComponent<Transform>(out transparentWall.transform))
            {
                Debug.Log("Wall position moved.");
                transparentWall.position = transparentWall.position - new Vector3(0, 5, 0);
            } else
            {
                Debug.Log("Wall not found.");
            }
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
