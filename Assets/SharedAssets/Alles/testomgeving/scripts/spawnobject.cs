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
        public GameObject platform;
        private Vector3 size;
        public int WallsCount;
        public List<GameObject> CloneWalls = new List<GameObject>();


        // Start is called before the first frame update
        void Start()
        {
            Vector3 surrounding = platform.transform.parent.transform.localScale;
            //transform.localScale = new Vector3(platform.transform.localScale.x * surrounding.x, 1, platform.transform.localScale.z * surrounding.z);
            size = platform.transform.localScale * surrounding.z;
            center = platform.transform.position;
            center.y += 0.8f;
            //center = platform.transform.position;
            //center.y += 0.8f;
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
            Debug.Log($"{center.x}  {center.z}");
            for (int i = 0; i < WallsCount; i++)
            {

                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                    Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                if (!Physics.CheckBox(pos, -pos))
                {
                    Transform WallClone = Instantiate(Wallprefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    //This changes the height of the instantiated clone randomly.
                    WallClone.localScale = new Vector3(WallClone.localScale.x ,Random.Range(2f, 12f), WallClone.localScale.z);
                    CloneWalls.Add(WallClone.gameObject);
                }

                //CloneWalls.AddRange(GameObject.FindGameObjectsWithTag("wall"));
                
            }
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

