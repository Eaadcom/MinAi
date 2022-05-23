using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
namespace testcar
{
    public class testCarAI : Agent
    {
        [SerializeField] private Material winColor;
        [SerializeField] private Material loseColor;
        [SerializeField] private MeshRenderer floorColor;
        private RandomizeGoal randomizeGoal;
        private int steps;
        void Awake()
        {
            randomizeGoal = gameObject.GetComponent<RandomizeGoal>();
        }

        public override void OnEpisodeBegin()
        {
            resetPosition();
            randomizeGoal.NewGoalPosition();
            steps = 0;
            Debug.Log("start");
        }

        public void resetPosition()
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
            var RB = GetComponent<Rigidbody>();
            RB.velocity = Vector3.zero;
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            float rotation = actions.ContinuousActions[0];
            float movement = actions.ContinuousActions[1];
            float moveSpeed = 8f;
            float rotateSpeed = 100f;
            if (movement != 0)
            {
                transform.Rotate(Vector3.up * rotation * (rotateSpeed * Time.deltaTime));
            }
            if (movement < 0)
            {
                transform.localPosition += -transform.forward * moveSpeed * Time.deltaTime;
            }
            else if (movement > 0)
            {
                transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        /*public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(targetTransform.localPosition);
        }*/

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            Debug.Log(continuousActions[0]);
            continuousActions[0] = Input.GetAxisRaw("Horizontal");
            continuousActions[1] = Input.GetAxisRaw("Vertical");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CarAiTarget>(out CarAiTarget target))
            {
                SetReward(+5f);
                floorColor.material = winColor;
                EndEpisode();
            }
            if (other.TryGetComponent<Wallarea>(out Wallarea wallarea))
            {
                SetReward(-5f);
                floorColor.material = loseColor;
                EndEpisode();
            }
        }
    }
}