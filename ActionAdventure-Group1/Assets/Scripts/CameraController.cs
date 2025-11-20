/************************************************************
* COPYRIGHT:  2025
* PROJECT: Action Adventure Game
* FILE NAME: CameraController.cs
* DESCRIPTION: Makes the camera follow the player.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/12 | Peyton Lenard | Created class
*
*
************************************************************/
 
using UnityEngine;
 

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(-15f, 20f, -15f);

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
        // Offset Camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
        
    } //end Update()
 
 
    /// <summary>
    /// A custom method example.
    /// </summary>
    /// <param name="exampleParameter"> A parameter that demonstrates passing data to the method.
    ///</param>
    private void CustomMethod(int exampleParameter)
    {
        
        
    }//end CustomMethod(int)
 
 
}//end CameraController
