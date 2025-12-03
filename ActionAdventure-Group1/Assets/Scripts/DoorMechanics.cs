/************************************************************
 * COPYRIGHT:  Year
 * PROJECT: Name of Project or Assignment
 * FILE NAME: PlayerKeys.cs
 * DESCRIPTION: Short Description of script.
 *
 * REVISION HISTORY:
 * Date [YYYY/MM/DD] | Author | Comments
 * ------------------------------------------------------------
 * 2000/01/01 | Your Name | Created class
 * 2025/12/01 | Chase Cone | Created class
 * 2024/12/3 | Chase Cone | Door opening mechanics
 ************************************************************/

using Unity.VisualScripting;
using UnityEngine;
 

public class DoorMechanics : MonoBehaviour
{
    public GameManager gameManager;
    public int keysRequired = 5;
    [SerializeField] private float topHeight = 10.0f;
    [SerializeField] private float speed = 2.0f;
    public bool doorOpen = false;
    [SerializeField] private bool finalDoor = false;

    void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager))
        {
            Debug.LogError($"{name} could not find GameManager component.");
        }
    }

    void Update()
    {
        if (doorOpen == true && transform.position.y < topHeight && !finalDoor)
        {
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        }
        else if (doorOpen == true && !finalDoor)
        {
            Destroy(gameObject);
        }
        else if (doorOpen == true && finalDoor)
        {
            gameManager.LoadNextLevel();
        }
        
    }
    
}