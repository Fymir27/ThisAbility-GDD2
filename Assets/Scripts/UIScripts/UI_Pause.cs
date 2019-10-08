using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : UIPanel
{
    public override void Activate()
    {
        base.Activate();
        Time.timeScale = 0;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Time.timeScale = 1;
    }

    private void Update()
    {
       
    }
}
