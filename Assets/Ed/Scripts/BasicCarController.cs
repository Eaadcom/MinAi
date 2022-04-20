using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[System.Serializable]
public class BasicAxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
     
public class BasicCarController : MonoBehaviour {
    public List<BasicAxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.rotation = rotation;
        // visualWheel.transform.position = newPosition;
    }

    public void Input(float movefoward, float steeringInput)
    {
        float motor = maxMotorTorque * movefoward;
        float steering = maxSteeringAngle * steeringInput;

        foreach (BasicAxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    public void resetPosition()
    {
        //transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-19,19), 0, UnityEngine.Random.Range(-19,19));
        transform.rotation = Quaternion.identity;
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, UnityEngine.Random.Range(0, 360), transform.eulerAngles.z);

        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }
}