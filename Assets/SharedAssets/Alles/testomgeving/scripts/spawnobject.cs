using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bigArena
{
    public class spawnobject : MonoBehaviour
    {
        public Transform Wallprefab;
        public GameObject Goalprefab;
        public Vector3 center;
        public float spawnCollisionCheck;
        public Vector3 size;
        public List<GameObject> CloneWalls = new List<GameObject>();


        // Start is called before the first frame update
        void Start()
        {
            SpawnGoal();
        }


        // Update is called once per frame
        void Update()
        {
            /* if(Input.GetKey(KeyCode.T))
             SpawnWall();*/
        }

        public void SpawnWall()
        {

            for (int i = 0; i < 15; i++)
            {
                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                if (!Physics.CheckBox(pos, -pos))
                {
                    Instantiate(Wallprefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                }

                CloneWalls.AddRange(GameObject.FindGameObjectsWithTag("wall"));

            }
        }

        public void SpawnGoal()
        {
            Goalprefab.transform.position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

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

