using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using FMOD.Studio;
using FMODUnity;
public class RadioTest : MonoBehaviour
{
    //public string nameOfSong;
    public AudioClip audioclip;
    public FMOD.Channel channel;
    public FMOD.ChannelGroup channGroup;
    void Start()
    {
        float[] samples = new float[audioclip.samples * audioclip.channels];
        audioclip.GetData(samples, 0);

        FMOD.Studio.System.create(out var lowlevel);
        lowlevel.getCoreSystem(out var coreSystem);

        uint lenbytes = (uint)(audioclip.samples * audioclip.channels * sizeof(float));

        FMOD.CREATESOUNDEXINFO soundinfo = new FMOD.CREATESOUNDEXINFO();
        soundinfo.length = lenbytes;
        soundinfo.format = FMOD.SOUND_FORMAT.PCMFLOAT;
        soundinfo.defaultfrequency = audioclip.frequency;
        soundinfo.numchannels = audioclip.channels;

        FMOD.RESULT result;
        FMOD.Sound sound;
        result = coreSystem.createSound("Name of Sound", FMOD.MODE.DEFAULT, ref soundinfo, out sound);

        IntPtr ptr1, ptr2;
        uint len1, len2;
        result = sound.@lock(0, lenbytes, out ptr1, out ptr2, out len1, out len2);
        Marshal.Copy(samples, 0, ptr1, (int)(len1 / sizeof(float)));
        if (len2 > 0)
        {
            Marshal.Copy(samples, (int)(len1 / sizeof(float)), ptr2, (int)(len2 / sizeof(float)));
        }
        result = sound.unlock(ptr1, ptr2, len1, len2);
        result = sound.setMode(FMOD.MODE.LOOP_NORMAL);
        coreSystem.createChannelGroup("Radio Channels", out channGroup);
        Debug.Log("Channel group MASTER: " + channGroup);
        //result = coreSystem.playSound(sound, , false, out channel);

    }
}