using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Speed")]
    [Range(0,10)]public float platformSpeed;

    [Header("Target Positions")]
    public Transform position1;
    public Transform position2;
    public Transform startPos;

    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = position1.position;
        
    }

    private void Update()
    {






        
        if (transform.position == position1.position)
        {
            nextPosition = position2.position;
        }

        if(transform.position == position2.position)
        {
            nextPosition = position1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, platformSpeed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1.position, position2.position);
        
        
    }

}
