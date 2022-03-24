using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class CarAI : Agent
{
    
    private CarController _controller;

    public override void Initialize()
    {
        _controller = GetComponent<CarController>();
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<float> _lastActions = actions.ContinuousActions;
        _controller.CurrentSteeringAngle = _lastActions[0];
        _controller.CurrentAcceleration = _lastActions[1];
        _controller.CurrentBrakeTorque = _lastActions[2];
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> _actionsOut = actionsOut.ContinuousActions;
        _actionsOut[0] = Input.GetAxis("Horizontal"); //left-right
        _actionsOut[1] = Input.GetAxis("Vertical"); //fowards-backwards
        _actionsOut[2] = Input.GetAxis("Jump"); //brakes
        
        
    }
    
}
