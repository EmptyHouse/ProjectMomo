using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform mainTarget;
    public float cameraLerpSpeed = 25f;
    public float minimumThresholdDistance = .01f;

    private Vector3 cameraOffset;

    #region monobehaviour methods
    private void Start()
    {
        mainTarget = this.transform.parent;
        cameraOffset = this.transform.position;
        this.transform.SetParent(null);

        SetUpCamera();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }
    #endregion monobehavoiur methods
    public void SetUpCamera()
    {

    }


    /// <summary>
    /// Moves the camera closer to the target based on the 
    /// </summary>
    private void UpdateCameraPosition()
    {
        Vector3 goalPosition = mainTarget.position + cameraOffset;
        Vector3 currentPosition = this.transform.position;
        if (Vector3.Distance(goalPosition, currentPosition) < minimumThresholdDistance)
        {
            this.transform.position = goalPosition;
            return; 
        }

        this.transform.position = Vector3.Lerp(currentPosition, goalPosition, Time.deltaTime * cameraLerpSpeed);
    }

    /// <summary>
    /// Performs a camera shake, based on the desired duration and intsensity desired
    /// </summary>
    /// <param name="cameraShakeDuration"></param>
    /// <param name="cameraShakeIntensity"></param>
    /// <returns></returns>
    public IEnumerator CameraShake(float cameraShakeDuration = .1f, float cameraShakeIntensity = 1)
    {
        float timeThatHasPassed = 0;
        while (timeThatHasPassed < cameraShakeDuration)
        {
            timeThatHasPassed += Time.deltaTime;
            yield return null;
        }
    }
}
