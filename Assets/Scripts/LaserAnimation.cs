﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var size = renderer.size;
        size.x = 0.8f + Mathf.PingPong(Time.time, 0.3f);
        renderer.size = size;
    }
}
