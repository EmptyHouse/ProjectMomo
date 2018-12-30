using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    #region static references
    private CameraShake instance;
    public CameraShake Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CameraShake>();
            }

            return instance;
        }
    }
    #endregion static references

    public float power = 0.7f;
    public float duration = 1f;
    [Range(0f, 1f)]
    public float severity = 1f;
    public Transform cameraTransformReference;
    public AnimationCurve powerFadeAnimationCurve;
    public AnimationCurve severityAnimationCurve;


    public bool isShaking { get; private set; }

    private Vector3 startPosition;
    private float initialDuration;

    private void Start()
    {
        cameraTransformReference = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shake();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Shake()
    {
        Shake(power, duration, severity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="power"></param>
    /// <param name="duration"></param>
    private void Shake(float power, float duration, float severity)
    {
        StartCoroutine(ShakeCoroutine(power, duration, severity));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="powerOfShake"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator ShakeCoroutine(float powerOfShake, float duration, float severity = 1f)
    {
        isShaking = false;//This will cancel any other camera shake even that is occuring.
        float timeToReachGoalPosition = 1 - severity;
        yield return null;
        
        isShaking = true;
        float timeThatHasPassed = 0;
        float timeRemainingToReachGoal = 0;
        Vector3 previousPosition = Vector3.zero;
        Vector3 localGoalPosition = Vector3.zero;
        while (timeThatHasPassed < duration && isShaking)
        {
            if (timeRemainingToReachGoal <= 0)
            {
                timeRemainingToReachGoal = timeToReachGoalPosition;
                cameraTransformReference.localPosition = localGoalPosition;
                previousPosition = cameraTransformReference.localPosition;
                localGoalPosition = Random.insideUnitCircle * powerOfShake * powerFadeAnimationCurve.Evaluate(timeThatHasPassed / duration);
            }
            else if (timeRemainingToReachGoal > 0)
            {
                cameraTransformReference.localPosition = Vector3.Lerp(previousPosition, localGoalPosition, (timeToReachGoalPosition - timeRemainingToReachGoal) / timeToReachGoalPosition);
                timeRemainingToReachGoal -= CustomTime.DeltaTime;
            }
            
            timeThatHasPassed += CustomTime.DeltaTime;
            yield return null;
        }
        cameraTransformReference.localPosition = Vector3.zero;
        isShaking = false;
    }
}
