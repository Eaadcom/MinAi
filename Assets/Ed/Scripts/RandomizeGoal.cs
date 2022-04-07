using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RandomizeGoal : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    public void NewGoalPosition()
    {
        int numx = 0;
        int numy = 0;
        Vector3 newposition = new Vector3(0, 0, 0);
        System.Random rnd = new System.Random();
        for (int j = 0; j < 1; j++)
        {
            if (j==0)
            {
                numx = rnd.Next(-18,18);
            }
                numy = rnd.Next(5,35);
        }

        targetTransform.transform.position = new Vector3(numx, 0, numy);
    }

}
