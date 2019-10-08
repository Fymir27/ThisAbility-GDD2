using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{

    public static PostProcessingController instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        volume = GetComponent<PostProcessVolume>();
    }

    PostProcessVolume volume;

    [SerializeField]
    PostProcessProfile blindProfile;
    [SerializeField]
    PostProcessProfile otherProfile;

    void Start()
    {
        volume.profile = otherProfile;
    }

    void Update()
    {
        
    }

    public void BlindPPActivate()
    {
        volume.profile = blindProfile;
    }
    public void OtherPPActivate()
    {
        volume.profile = otherProfile;
    }
}
