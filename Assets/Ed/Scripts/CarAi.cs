using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAi : Agent
{
    [SerializeField] 
    private Transform targetTransform;

    [SerializeField] private MeshRenderer floorColor;
    [SerializeField] private Material winColor;
    [SerializeField] private Material loseColor;

    private BasicCarController carcontroller;

    private RandomizeGoal randomizeGoal;
    void Awake()
    {
        carcontroller = gameObject.GetComponent<BasicCarController>();
        randomizeGoal = gameObject.GetComponent<RandomizeGoal>();
    }
    
    public override void OnEpisodeBegin()
    {
        carcontroller.resetPosition();
        randomizeGoal.NewGoalPosition();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation(transform.localPosition);
        // sensor.AddObservation(targetTransform.localPosition);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        float previousDistance = Vector3.Distance(carcontroller.transform.localPosition, randomizeGoal.transform.localPosition);
        
        float movefoward = actions.ContinuousActions[0];
        float turn = actions.ContinuousActions[1];

        carcontroller.Input(movefoward, turn);
        
        float currentDistance = Vector3.Distance(carcontroller.transform.localPosition, randomizeGoal.transform.localPosition);

        // Reward or Penalty
        if (currentDistance < previousDistance)
        {
            SetReward(+0.001f);
        } else {
            SetReward(-0.001f);
        }
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float movefoward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        print(movefoward);

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = movefoward;
        continuousActions[1] = turn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarAiTarget>(out CarAiTarget target))
        {
            SetReward(+10f);
            floorColor.material = winColor;
            EndEpisode();
        }
        if (other.TryGetComponent<Wallarea>(out Wallarea wallarea))
        {
            SetReward(-10f);
            floorColor.material = loseColor;
            EndEpisode();
        }
    }
}
