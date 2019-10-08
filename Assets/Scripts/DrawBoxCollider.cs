using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxCollider : MonoBehaviour
{
    [SerializeField]
    Color color;

    [SerializeField]
    bool showAlways = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if (showAlways)
            return;

        var coll = GetComponent<Collider2D>();
        var oldColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawCube(coll.bounds.center, coll.bounds.size);
        Gizmos.color = oldColor;
    }

    private void OnDrawGizmos()
    {
        if (!showAlways)
            return;

        var coll = GetComponent<Collider2D>();
        var oldColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawCube(coll.bounds.center, coll.bounds.size);
        Gizmos.color = oldColor;
    }
}
