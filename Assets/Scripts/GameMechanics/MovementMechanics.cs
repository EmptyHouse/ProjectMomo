using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMechanics : MonoBehaviour {

    #region main variables

    [Header("Ground Movement")]
    [Tooltip("The maximum walking speed")]
    public float walkingSpeed = 2f;
    [Tooltip("The maximum running speed")]
    public float runningSpeed = 5f;
    [Tooltip("The units per second that our speed will increase")]
    public float acceleration = 25f;

    /// <summary>
    /// The current speed or our character
    /// </summary>
    private float currentSpeed;

    #endregion main variables

    #region monobehaivour methods
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    #endregion monobehaviour methods

    /// <summary>
    /// Updates the speed of our character while they are grounded
    /// </summary>
    private void UpdateCurrentSpeedOnGround()
    {

    }
}
