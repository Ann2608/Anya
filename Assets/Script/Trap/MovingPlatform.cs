using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform UpMv;
    [SerializeField] private Transform DownMv;
    public float Speed;

    public Vector3 nextPosition;

    private void Start()
    {
        nextPosition = DownMv.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, Speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, nextPosition) < 0.01f)
            nextPosition = (nextPosition == UpMv.position) ? DownMv.position : UpMv.position;
    }
}
