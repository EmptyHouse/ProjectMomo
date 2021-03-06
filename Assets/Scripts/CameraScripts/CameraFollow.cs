﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this to the main camera of our game to follow the current player. This can also be used to follow other assigned targets if necessary
/// </summary>
public class CameraFollow : MonoBehaviour {
    public Transform mainTarget;
    private CustomPhysics2D targetRigidbodyPhysics;

    public Vector2 offsetFromVelocity;


    private Vector3 cameraOffset;
    private Camera associatedCamera;

    #region camera smooth damp
    public float dampTime = .15f;
    public Vector3 refVel;
    #endregion camera smooth damp
    #region monobehaviour methods
    private void Start()
    {
        mainTarget = this.transform.parent;
        targetRigidbodyPhysics = mainTarget.GetComponent<CustomPhysics2D>();
        cameraOffset = this.transform.position - mainTarget.position;
        this.transform.SetParent(null);
        associatedCamera = GetComponentInChildren<Camera>();

        GameOverseer.Instance.AddObjectToDontDestroyOnLoad(this.gameObject);

    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }
    #endregion monobehavoiur methods

    /// <summary>
    /// Moves the camera closer to the target based on the 
    /// </summary>
    private void UpdateCameraPosition()
    {
        if (mainTarget)
        {
            Vector3 adjustedVelocityOffset = new Vector2(targetRigidbodyPhysics.velocity.x * offsetFromVelocity.x, targetRigidbodyPhysics.velocity.y * offsetFromVelocity.y);
            Vector3 goalPosition = mainTarget.position + cameraOffset + adjustedVelocityOffset;

            Vector3 delta = goalPosition - transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + delta, ref refVel, dampTime);
        }
    }
}
