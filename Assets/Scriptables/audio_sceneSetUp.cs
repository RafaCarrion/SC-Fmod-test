using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(fileName = "Scene SetUp", menuName = "ScriptableObjects/AudioSceneScriptableObject", order = 1)]
public class audio_sceneSetUp : ScriptableObject
{
	#region BankLoading
	private StudioBankLoader bankLoader;
	public List<string> sceneBank;
	#endregion

	public EventReference[] eventsOnStart;

	private EventInstance[] fmodInstances;
	public void StartScene()
	{
		
		fmodInstances = new EventInstance[eventsOnStart.Length];
		for (int i = 0; i < eventsOnStart.Length; i++)
		{
			fmodInstances[i] = RuntimeManager.CreateInstance(eventsOnStart[i]);
			fmodInstances[i].start();
		}
	}

	public void Loadbank()
	{
		bankLoader = new StudioBankLoader();
		bankLoader.Banks = sceneBank;
		bankLoader.Load();
	}
}
