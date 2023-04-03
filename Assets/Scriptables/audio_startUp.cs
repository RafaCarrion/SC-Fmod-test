using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_startUp : MonoBehaviour
{
    public audio_sceneSetUp audio_SetUp;
    // Start is called before the first frame update
    void Start()
    {
        audio_SetUp.StartScene();
    }
}
