using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activator : MonoBehaviour
{
    [SerializeField]
    public bool on;

    /// <summary>
    /// Call this in Start!
    /// </summary>
    protected void Register()
    {
        // this is so the controller can reset everything on level restart
        Controller.Instance.Activators.Add(new System.Tuple<Activator, bool>(this, on));
    }
}

[System.Serializable]
public class ActivatorArray
{
    [SerializeField]
    public List<Activator> activators;
}
