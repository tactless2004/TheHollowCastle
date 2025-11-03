/*******************************************************************
* COPYRIGHT:  2025
* PROJECT: Sandbox-Movement
* FILE NAME: MoveTransform.cs
* DESCRIPTION: Moves a GameObject with transform.position based on speed and direction.
*
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* -----------------------------------------------------------------
* 2025/09/18 | Akram Taghavi-Burris | Initial class created
* 2025/09/22 | Noah Zimmerman | Added additional control of movement
*
/******************************************************************/ 


using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    // Serialized fields for initial values
    
    [Header("MOVEMENT SETTINGS")]
    // Direction of movement
    [SerializeField]
    [Tooltip("Direction of movement. Will be normalized automatically to ensure consistent movement.")]
    private Vector3 _direction = Vector3.right; 

    // Constant Max Speed
    private const float MAX_SPEED = 10f;

    [SerializeField]
    [Range(0f, MAX_SPEED)]
    [Tooltip("Speed of the object’s movement. Cannot exceed maximum speed.")]
    private float _speed = 5f;
    
    [SerializeField]
    [Tooltip("Should the object be moving on initialization?")]
    private bool _moveOnAwake = true;

    // Runtime movement flag
    private bool _isMoving;
    
    #if UNITY_EDITOR
    [Header("FOR TESTING ONLY")]
    [SerializeField] 
    private bool _enableEditorTesting = false;
    
    //Enum for list of possible actions to test 
    private enum TestAction
    {
        None,   // No action selected
        Move,   // Run Move() test
        Stop    // Run Stop() test
    }
    
    [SerializeField]
    [Tooltip("Select which action to test in the Editor.")]
    private TestAction _testAction = TestAction.None;
    #endif
    
    // Public properties with encapsulation
    
    public float Speed
    {
        get => _speed; 
        set => _speed = Mathf.Clamp(value, 0f, MAX_SPEED);
    }
    
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value.normalized;
    }
    
    // Awake is called once on initialization         
    private void Awake()
    {
        // Validate initial speed and direction via properties
        Speed = _speed;
        Direction = _direction;
        
        // Determine if the object should start moving
        _isMoving = _moveOnAwake;
      
    }//end Awake()

    private void Update()
    {
        #if UNITY_EDITOR
        if (_enableEditorTesting)
        {
            RunMovementTest();
        }//end if(_enableEditorTesting)
        #endif
        if(_isMoving)
        {
            Move();

        }//end if(_isMoving)

    }//end Update()
     
    /// <summary>
    /// Moves the object in a specified direction at a specified speed.
    /// </summary>
    /// <param name="direction">The direction to move the object (optional).</param>
    /// <param name="speed">The speed at which the object should move (optional).</param>
    public void Move(Vector3? direction = null, float? speed = null)
    {
        // Resolve the effective values for this frame
        Vector3 moveDirection = direction ?? Direction;
        float moveSpeed = speed ?? Speed;

        // Update properties to ensure validation and internal consistency
        Direction = moveDirection;
        Speed = moveSpeed;

        //Debug.Log("Direction: " + Direction);
        //Debug.Log("Speed " + Speed);

        // Flags the object as moving
        _isMoving = true;
        
        // Move the GameObject using the resolved frame values
        transform.position += Speed * Time.deltaTime * Direction;

    }//end Move()
    
    /// <summary>
    /// Stops the object's movement by updating the movement flag.
    /// </summary>
    public void Stop()
    {
        // Flags the object as stopped
        _isMoving = false;

    }//end Stop()

    #if UNITY_EDITOR    
    /// <summary>
    /// Runs the selected movement test in the Editor based on the _testAction enum.
    /// </summary>
    private void RunMovementTest()
    {
        switch (_testAction)
        {
            case TestAction.Move:
                Debug.Log("Testing Move");
                Move();
                break;

            case TestAction.Stop:
                Debug.Log("Testing Stop");  
                Stop();
                break;

            case TestAction.None:
                Debug.Log("Testing None");
                //Do nothing
                break;
                
            default:
                Debug.Log("Unhandled TestAction: " + _testAction); 
                break;

        }//end switch(_testAction)

    }//end RunMovementTest()
    #endif
    
}//end MoveTransform