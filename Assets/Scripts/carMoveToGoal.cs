using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class carMoveToGoal : Agent
{
    
    [SerializeField] 
    private Transform targetTransform;
    
    private float moveSpeed = 0f;
    private int turning = 0;
    
    
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        // float moveX = actions.ContinuousActions[0];
        // float moveY = actions.ContinuousActions[1];

        // go left, right or don't turn
        int steering = actions.DiscreteActions[1];

        // accelerate or decelerate
        int acceleration = actions.DiscreteActions[0];
        
        // don't turn
        if (steering == 0)
        {
            turning = 0;
        }
        // go right
        if (steering == 1)
        {
            turning = 1; 
        }
        // go left
        if (steering == 2)
        {
            turning = -1;
        }

        if (acceleration == 0) { }
        if (acceleration == 1) { }

        // transform.localPosition += new Vector3(moveX, 0, moveY) * Time.deltaTime * moveSpeed;
        
        // transform.localPosition += new Vector3()
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        // continuousActions[0] = Input.GetAxisRaw("Horizontal");
        // continuousActions[1] = Input.GetAxisRaw("Vertical");

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        
        print(discreteActions);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
        
    }
    
}
