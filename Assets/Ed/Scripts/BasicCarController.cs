using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        //position = position.Set(position.x, position.y, position.z+90);

        float rotaW = rotation.w;
        float rotaX = rotation.x;
        float rotaY = rotation.y;
        float rotaZ = rotation.z;

        Quaternion newRotation = new Quaternion(rotaX, rotaY, rotaZ, 180f);
        
        visualWheel.transform.rotation = newRotation;
        visualWheel.transform.position = position;
    }
     
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
     
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
}