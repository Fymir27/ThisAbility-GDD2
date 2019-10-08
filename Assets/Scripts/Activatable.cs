using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

/// <summary>
/// interface for objects that are activatable by levers, buttons etc.
/// for example: doors, moving platforms etc.
/// </summary>
public abstract class Activatable : MonoBehaviour
{
    //public enum Mode
    //{
    //    OR,
    //    AND
    //}

    //[SerializeField]
    //int activationCount = 0;

    //[SerializeField]
    //Activator[] activators;

    /// <summary>
    /// ALL the activators in a single ActivatorArray are AND-connected
    /// -> ALL the activators in there need to be on!
    /// The ActivatorArrays in this list are OR-connected
    /// -> at least ONE of the ActivatorArrays needs to be ALL true
    /// </summary>
    [SerializeField]
    List<ActivatorArray> activatorsOR;

    //[SerializeField]
    //Mode mode;

    [SerializeField]
    public bool on = false;

    [SerializeField]
    public bool invert = false;

    /// <summary>
    /// Call this every update!
    /// </summary>
    protected void CheckStatus()
    {
        if(IsActive())
        {
            //Debug.Log("active");
            if(!on) Activate();
        }
        else
        {
            //Debug.Log("inactive");
            if (on) Deactivate();
        }
    }

    private bool IsActive()
    {
        return activatorsOR.Any(activatorsAND => 
               activatorsAND.activators.All(activator => 
               activator.on)) ^ invert;

        /*

        bool activated;

        switch(mode)
        {
            case Mode.AND:
                activated = true;
                foreach (var a in activators)
                {
                    activated = activated && a.on;
                }
                return activated;

            case Mode.OR:
                activated = false;
                foreach (var a in activators)
                {
                    activated = activated || a.on;
                }
                return activated;

            default:
                return false;
        }
        */
    }

    public void Activate()
    {
        Debug.Log("Activatable Activate");
        on = true;
        OnActivate();
    }

    public void Deactivate()
    {
        Debug.Log("Activatable Deactivate");
        on = false;
        OnDeactivate();
    }

    protected abstract void OnActivate();
    protected abstract void OnDeactivate(); 
}
