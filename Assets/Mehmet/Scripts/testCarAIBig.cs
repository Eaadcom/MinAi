using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
namespace testcar
{
    public class testCarAIBig : Agent
    {
        [SerializeField] private Material winColor;
        [SerializeField] private Material loseColor;
        [SerializeField] private MeshRenderer floorColor;
        public bigArena.spawnobject Spawnobject;
        private int steps;
        private float distance;


        public override void OnEpisodeBegin()
        {
            Spawnobject.Kill();
            resetPosition();

            //Spawnwall
            Spawnobject.SpawnGoal();
            Spawnobject.SpawnWall();

            steps = 0;
            distance = Vector3.Distance(transform.localPosition, Spawnobject.Goalprefab.transform.localPosition);
            Debug.Log("start");
        }
        public void Awake()
        {
            //Spawnobject = GameObject.FindGameObjectWithTag("Respawn").GetComponent<bigArena.spawnobject>();
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
            //float rotation = actions.ContinuousActions[0];
            //float movement = actions.ContinuousActions[1];
            float rotation = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
            float movement = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;
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
                //Try out training while rewarding getting closer to the goal.
                float newDistance = Vector3.Distance(transform.localPosition, Spawnobject.Goalprefab.transform.localPosition);
                if(newDistance < this.distance)
                {
                    AddReward(.0002f);
                }
                else
                {
                    AddReward(-.0002f);
                }

                this.distance = newDistance;
            }
            //AddReward(-0.0001f);
        }

        /*public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(targetTransform.localPosition);
        }*/

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            /*ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            Debug.Log(continuousActions[0]);
            continuousActions[0] = Input.GetAxisRaw("Horizontal");
            continuousActions[1] = Input.GetAxisRaw("Vertical");
            */
            ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
            int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            discreteActions[0] = horizontal >= 0 ? horizontal : 2;
            discreteActions[1] = vertical >= 0 ? vertical : 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CarAiTarget>(out CarAiTarget target))
            {
                SetReward(+5f);
                //floorColor.material = winColor;
                //Spawnobject.Kill();
                EndEpisode();
            }
            if (other.TryGetComponent<bigArena.Wall>(out bigArena.Wall wallarea))
            {
                SetReward(-5f);
                //floorColor.material = loseColor;
                //Spawnobject.Kill();
                EndEpisode();
            }
        }
    }
}
