using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCompanionMovement : MonoBehaviour {

    #region main variables
    [Tooltip("This is the target position that our companion will try to move toward")]
    public Transform targetTransform;
    [Tooltip("If there is an offset that you would like to place on the target position of the companion, you can set that with this variable")]
    public Vector2 targetOffset;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private float currentSpeed;
    private float timeThatHasPassed;

    [Tooltip("This is the maximum velocity at which out companion will begin a hovering animation")]
    public float velocityThresholdBeforeHover = .2f;
    [Tooltip("The intensity of our companion's floating")]
    public float hoverMagnitude = .2f;
    [Tooltip("The speed at which out companion will float")]
    public float hoverSpeed = 1;
    private Vector3 offsetForHover = Vector3.zero;
    #endregion main variables

    #region monobehaviour methods 
    private void Awake()
    {
        this.transform.SetParent(null);
        this.transform.position = targetTransform.position;
    }

    private void Update()
    {
        timeThatHasPassed += CustomTime.DeltaTime;
        if (timeThatHasPassed > 100)//Simply so that the number doesn't get too big if we have left it running for days... floats get less accurate the bigger they are.
        {
            timeThatHasPassed -= 100;
        }

        Vector3 targetPosition = targetTransform.position + new Vector3(targetOffset.x, targetOffset.y);
        
        Vector3 currentPosition = transform.position;
        if (velocity.magnitude < velocityThresholdBeforeHover)
        {

            currentPosition -= offsetForHover;
        }
        else
        {
            timeThatHasPassed = 0;
        }
        Vector3 updatedPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
        updatedPosition.z = this.transform.position.z;

        offsetForHover = CalculateOffsetFromHover();

        this.transform.position = updatedPosition + offsetForHover;
    }
    #endregion monobehaviour methods

    private Vector3 CalculateOffsetFromHover()
    {
        timeThatHasPassed += CustomTime.DeltaTime;
        return Vector3.up * Mathf.Sin(timeThatHasPassed * hoverSpeed) * hoverMagnitude;

    }
}
