using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using FMODUnity;
using Unity.VisualScripting;

public class RadioCallback : MonoBehaviour
{
    public EventReference radioEvent;
    [Tooltip("Si es por string hay que poner .wav .mp3 ....")] public string songName;

    FMOD.Studio.EVENT_CALLBACK radioCallback;
    FMOD.Studio.EventInstance ins_radioEvent;
    

    private void Start()
    {
        radioCallback = new FMOD.Studio.EVENT_CALLBACK(RadioEventCallback); // create the delegate before setting the callback
        PlaySong(songName);
    }


    private void PlaySong(string key)
    {
        var radio_event_Instance = FMODUnity.RuntimeManager.CreateInstance(radioEvent);

        // Pin the key string in memory and pass a pointer through the user data
        GCHandle stringHandle = GCHandle.Alloc(key, GCHandleType.Pinned);
        radio_event_Instance.setUserData(GCHandle.ToIntPtr(stringHandle));

        radio_event_Instance.setCallback(radioCallback);
        radio_event_Instance.start();
        radio_event_Instance.release();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT RadioEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        // Lo hago con los pointers
        FMOD.Studio.EventInstance instancia = new FMOD.Studio.EventInstance(instancePtr); //creo instancia evento a traves del puntero
        IntPtr instancePtr_temp;
        FMOD.RESULT result = instancia.getUserData(out instancePtr_temp); //puntero a los datos del usuario

        UnityEngine.Debug.Log("Callback type: " + type.ToString());
        switch (type) {
            case FMOD.Studio.EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND:
                {
                    UnityEngine.Debug.Log("Programmer Sound has been triggered");

                    /////////
                    FMOD.MODE soundMode = FMOD.MODE.LOOP_NORMAL | FMOD.MODE.CREATECOMPRESSEDSAMPLE | FMOD.MODE.NONBLOCKING;
                    var parameter = (FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES));

                    FMOD.Sound songSound;

                    // Get the string object
                    GCHandle stringHandle = GCHandle.FromIntPtr(instancePtr_temp);
                    String key = stringHandle.Target as String;


                    var soundResult = FMODUnity.RuntimeManager.CoreSystem.createSound(Application.streamingAssetsPath + "/" + key, soundMode, out songSound);
                    if (soundResult == FMOD.RESULT.OK)
                    {
                        parameter.sound = songSound.handle;
                        parameter.subsoundIndex = -1;
                        Marshal.StructureToPtr(parameter, parameterPtr, false);
                    }
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED:
                {
                    UnityEngine.Debug.Log("Programmer Sound has been destroyed");
                }
                break;
        }
        return FMOD.RESULT.OK;
    }
}
