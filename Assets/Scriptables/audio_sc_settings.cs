using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

[CreateAssetMenu(fileName = "Scene SetUp", menuName = "ScriptableObjects/Audio Settings", order = 2)]
public class audio_sc_settings : ScriptableObject
{
	[Header("VOLUME SETTINGS")]
	public CustomBus[] m_bus;

	public void LoadSettings()
	{
		for (int i = 0; i < m_bus.Length; i++)
		{
			Bus	bus = RuntimeManager.GetBus(m_bus[i].name);
			float volume = 1 + (m_bus[i].vol / 80);
			bus.setVolume(volume);
			Debug.Log("Setting bus " + m_bus[i].name + " to vol: " + volume.ToString());
		}
	}
}

[Serializable] 
public class CustomBus
{
	[Tooltip("bus:/ will be automatically added")]
	public string name;
	[Range(-80f , 0)]
	public float vol;

}
