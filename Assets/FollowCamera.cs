using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    Vector3 offset = Vector3.zero;

    private void Awake()
    {
        offset = target.position-transform.position;
    }

    void Update()
    {
        this.transform.position = target.position - offset;        
    }
}
