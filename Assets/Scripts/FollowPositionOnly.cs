using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionOnly : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }

    public void Init(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
        //Debug.Log("Target: " + target.gameObject.name);
    }
}
