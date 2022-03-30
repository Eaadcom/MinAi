using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
namespace SceneMehmet
{
    public class CarAI : Agent
    {
        [SerializeField] public Transform carTransform;
        public override void OnEpisodeBegin()
        {
            carTransform.localPosition = Vector3.zero;
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(carTransform.localPosition);
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            float rotation = actions.ContinuousActions[0];
            float movement = actions.ContinuousActions[1];
            float moveSpeed = 3f;
            float rotateSpeed = 100f;
            if(movement != 0){
                carTransform.Rotate(Vector3.up * rotation * (rotateSpeed *Time.deltaTime));
            }
            if(movement < 0 ){
                carTransform.localPosition += -carTransform.forward * moveSpeed * Time.deltaTime;
            }
            else if(movement > 0){
                carTransform.localPosition += carTransform.forward * moveSpeed * Time.deltaTime;
            }
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = Input.GetAxisRaw("Horizontal");
            continuousActions[1] = Input.GetAxisRaw("Vertical");
        }
        private void OnTriggerEnter(Collider other)
        {

        }
    }
}
