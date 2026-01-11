/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: Singleton.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
* 2025/11/4 | Chase Cone | Created class from tutorial
*
************************************************************/
 
using UnityEngine;

// Generic Singleton base class that any MonoBehaviour can inherit from
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] [Tooltip("Is the game object persistent through scenes.")]
    private bool _isPersistant = true;


    // Static instance that holds the reference to the Singleton
    public static T Instance { get; private set; }

    // Unity's Awake method, called when the script instance is being loaded
    private void Awake()
    {
        // Check for singleton duplication
        CheckForSingleton();

    } //end Awake()

    // Ensures that only one instance of the Singleton exists
    void CheckForSingleton()
    {
        // If no instance exists, assign this instance
        if (Instance == null)
        {
            Instance = this as T;
            CheckForPersistance();
        }
        // If an instance already exists and it's not this one, destroy the new instance to maintain the Singleton
        else
        {
            // Ensure that only the original Singleton instance persists
            Destroy(gameObject);
        }

    } //end CheckForSingleton()

    void CheckForPersistance()
    {
        // Check if persistence is required
        if (_isPersistant)
        {
            // Detach from parent if there is one
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }

            // Mark this GameObject as not to be destroyed
            DontDestroyOnLoad(gameObject);

        } 


    } //end CheckForPersitance()
} //end Singleton
