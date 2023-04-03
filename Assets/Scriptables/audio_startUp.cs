using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_startUp : MonoBehaviour
{
    public audio_sc_sceneSetUp audio_SetUp;

    private bool runTime = false; //los bus necesitan ser cambiados en runtime
    // Start is called before the first frame update
    void Start()
    {
        audio_SetUp.StartScene();
    }

	private void Update()
	{
		if (!runTime)
		{
            try
            {
                audio_sc_settings audioSettings = Resources.Load<audio_sc_settings>("Audio Settings");
                audioSettings.LoadSettings();
                runTime = true;
            }
            catch (Exception e)
            {
                Debug.LogError("Error Loading AudioSettings from resources: " + e.Message);
            }
        }
            
    }

}
