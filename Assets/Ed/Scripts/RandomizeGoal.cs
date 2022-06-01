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
        if (targetTransform != null)
        {
            targetTransform.localPosition = new Vector3(UnityEngine.Random.Range(-19, 19), targetTransform.localPosition.y, UnityEngine.Random.Range(-19, 19));
        }
    }

}
