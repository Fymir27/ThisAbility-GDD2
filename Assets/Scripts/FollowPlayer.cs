using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform transformPlayer;

    public static FollowPlayer Instance;

    //[SerializeField]
    //Bounds offset;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    // camera stays fixed when this is true
    bool frozen = false;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //offset.center = transform.position - Vector3.back + Vector3.up * 10;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!frozen)
        //{
        //    transform.position = Controller.Instance.ActiveCharacter.transform.position + offset;
        //}

        /*
        offset.center = transform.position - Vector3.back; 
        Debug.DrawLine(offset.min, offset.max, Color.red);
        Debug.DrawLine(offset.center + new Vector3(-offset.extents.x, offset.extents.y),
                       offset.center + new Vector3(offset.extents.x, -offset.extents.y), Color.red);
        Vector3 pos = transform.position;
        if(transformPlayer.position.x < pos.x - offset.extents.x)
        {
            pos.x = transformPlayer.position.x + offset.extents.x;
        }
        else if (transformPlayer.position.x > pos.x + offset.extents.x)
        {
            pos.x = transformPlayer.position.x - offset.extents.x;
        }

        if (transformPlayer.position.y < pos.y - offset.extents.y)
        {
            pos.y = transformPlayer.position.y + offset.extents.y;
        }
        else if (transformPlayer.position.y > pos.y + offset.extents.y)
        {
            pos.y = transformPlayer.position.y - offset.extents.y;
        }

        transform.position = pos;
        */
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }
}
