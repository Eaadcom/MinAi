using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    public spawnobject Spawnobject;

    public override void OnEpisodeBegin(){
        Spawnobject=GameObject.FindGameObjectWithTag("Respawn").GetComponent<spawnobject>();
        transform.localPosition = Vector3.zero;
        Spawnobject.SpawnGoal();
       
        Spawnobject.SpawnWall();
        
        
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 20f;
        transform.localPosition += new Vector3(moveX, 0 , moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<Goal>(out Goal goal)){
        SetReward(1f);
        floorMeshRenderer.material = winMaterial;
        Spawnobject.Kill();
        EndEpisode();
        
        }
        if(other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            Spawnobject.Kill();
            EndEpisode();
            
        }
    }
    
}
