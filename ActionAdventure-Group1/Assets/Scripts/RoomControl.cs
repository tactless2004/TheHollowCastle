/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: RoomControl.cs
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
using System.Collections;
 

public class RoomControl : MonoBehaviour
{
    [SerializeField]
    private Transform wallA;


    // Awake is called once on initialization (before Start)
    private void Awake()
    {
        if(TryGetComponent<Transform>(out wallA))
        {
            Debug.Log("Wall A position data stored.");
            //Vector3 positionA = wallA.position;
        } else
        {
            Debug.Log("Wall A not found.");
        }
        
        
    } //end Awake()
 
 
    // Start is called once before the first Update
    private void Start()
    {
        //Transform wallA = transform.Find("OuterWall");
        
    } //end Start()
 
 
    // Update is called once per frame
    private void Update()
    {
        
        
    } //end Update()

    private void OnTriggerEnter(Collider Player)
    {
        Debug.Log("Player has entered the room.");
        
        if(TryGetComponent<Transform>(out wallA))
        {
            Debug.Log("Wall A position moved.");
            wallA.position = wallA.position + new Vector3(0, 3, 0);
        } else
        {
            Debug.Log("Wall A not found.");
        }

        // if (wallA != null)
        // {
        //     Debug.Log("Wall A position moved.");
        //     wallA.position = wallA.position + new Vector3(0, 3, 0);
        // } else
        // {
        //     Debug.Log("Wall A not found.");
        // }
    } //end OnTriggerEnter(Collider)

    private void OnTriggerExit(Collider Player)
    {
        Debug.Log("Player has left the room.");
        
        if(TryGetComponent<Transform>(out wallA))
        {
            Debug.Log("Wall A position returned.");
            wallA.position = wallA.position - new Vector3(0, 3, 0);
        } else
        {
            Debug.Log("Wall A not found.");
        }

        // if (wallA != null)
        // {
        //     Debug.Log("Wall A position returned.");
        //     wallA.position = wallA.position - new Vector3(0, 3, 0);
        // } else
        // {
        //     Debug.Log("Wall A not found.");
        // }
    } //end OnTriggerExit(Collider)
 
 
    /// <summary>
    /// A custom method example.
    /// </summary>
    /// <param name="exampleParameter"> A parameter that demonstrates passing data to the method.
    ///</param>
    private void CustomMethod(int exampleParameter)
    {
        
        
    }//end CustomMethod(int)
 
 
}//end RoomControl
