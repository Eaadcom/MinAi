using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace bigArena
{
    public class spawnobject : MonoBehaviour
    {
        public Transform Wallprefab;
        public GameObject Goalprefab;
        public Vector3 center;
        public float spawnCollisionCheck;
        public GameObject platform;
        private Vector3 size;
        public int WallsCount;
        public List<GameObject> CloneWalls = new List<GameObject>();
        private float curriculumLesson;


        // Start is called before the first frame update
        void Start()
        {
            /*Vector3 surrounding = platform.transform.parent.transform.localScale;
            //transform.localScale = new Vector3(platform.transform.localScale.x * surrounding.x, 1, platform.transform.localScale.z * surrounding.z);
            size = platform.transform.localScale * surrounding.z;
            center = platform.transform.position;
            center.y += 0.8f;
            //center = platform.transform.position;
            //center.y += 0.8f;*/
            float arenaScale = Academy.Instance.EnvironmentParameters.GetWithDefault("arenaScale", 1f);
            this.curriculumLesson = Academy.Instance.EnvironmentParameters.GetWithDefault("curriculumLesson", 1f);
            platform.transform.parent.transform.localScale = new Vector3(arenaScale, 1, arenaScale);
            size = platform.transform.localScale * platform.transform.parent.transform.localScale.z;
            center = platform.transform.position;
            center.y += 0.8f;
            SpawnGoal();
        }

        public void ResetArena()
        {
            float arenaScale = Academy.Instance.EnvironmentParameters.GetWithDefault("arenaScale", 1f);
            this.curriculumLesson = Academy.Instance.EnvironmentParameters.GetWithDefault("curriculumLesson", 1f);
            platform.transform.parent.transform.localScale = new Vector3(arenaScale, 1, arenaScale);
            size = platform.transform.localScale * platform.transform.parent.transform.localScale.z;
            center = platform.transform.position;
            center.y += 0.8f;

            ArenaManager();
        }

        // Update is called once per frame
        void Update()
        {
            /* if(Input.GetKey(KeyCode.T))
             SpawnWall();*/
        }

        public void ArenaManager()
        {
            WallsCount = 0;

            if (curriculumLesson == 1.0)
            {
                SpawnGoal();
            }

            if (curriculumLesson == 2.0)
            {
                SpawnGoal();
            }

            if (curriculumLesson == 3.0)
            {
                SpawnGoal();
            }
            
            if (curriculumLesson == 4.0)
            {
                SpawnGoal();
            }
            
            if (curriculumLesson == 5.0)
            {
                SpawnLessonOneWall();
                SpawnLessonOneGoal();
            }
            
            if (curriculumLesson == 6.0)
            {
                SpawnLessonOneWall();
                SpawnLessonTwoGoal();
            }
        }

        public void SpawnLessonZeroGoal()
        {
            Goalprefab.transform.position = center + new Vector3(25, 0, 20);
        }

        public void SpawnLessonTwoWall()
        {
            Vector3 pos = center + new Vector3(0, 0 ,-20);
            Transform WallClone = Instantiate(Wallprefab, pos, Quaternion.Euler(0, 0, 0));
            WallClone.localScale = new Vector3(WallClone.localScale.x, Random.Range(2f, 12f),
                WallClone.localScale.z);
            CloneWalls.Add(WallClone.gameObject);
        }

        public void SpawnLessonOneWall()
        {
            Vector3 pos = center + new Vector3(0, 0 ,35);
            Transform WallClone = Instantiate(Wallprefab, pos, Quaternion.Euler(0, 0, 0));
            WallClone.localScale = new Vector3(WallClone.localScale.x, Random.Range(2f, 12f),
                WallClone.localScale.z);
            CloneWalls.Add(WallClone.gameObject);
        }
        public void SpawnWall()
        {
            
            for (int i = 0; i < WallsCount; i++)
            {

                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                if (!Physics.CheckBox(pos, -pos))
                {
                    Transform WallClone = Instantiate(Wallprefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    //This changes the height of the instantiated clone randomly.
                    WallClone.localScale = new Vector3(WallClone.localScale.x, Random.Range(2f, 12f),
                        WallClone.localScale.z);
                    CloneWalls.Add(WallClone.gameObject);
                }

                //CloneWalls.AddRange(GameObject.FindGameObjectsWithTag("wall"));

            }
        }
        
        public void SpawnLessonOneGoal()
        {
            Goalprefab.transform.position = center + new Vector3(30, 0, 55);
        }
        
        public void SpawnLessonTwoGoal()
        {
            
            Goalprefab.transform.position = center + new Vector3(0, 0, 55);
            // if (Random.Range(1, 3) == 1)
            // {
            //     Goalprefab.transform.position = center + new Vector3(0, 0, 30);
            // }
            // else
            // {
            //     Goalprefab.transform.position = center + new Vector3(0, 0, -30);
            // }
        }

        public void SpawnGoal()
        {
            Goalprefab.transform.position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                .2f, Random.Range(-size.z / 2, size.z / 2));
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.localPosition + center, size);
        }

        public void Kill()
        {

            foreach (var clone in CloneWalls)
            {

                if (clone.ToString() == "Wall (8)(Clone) (UnityEngine.GameObject)")
                {
                    Destroy(clone);
                }
            }

            CloneWalls.Clear();
        }



    }
}

