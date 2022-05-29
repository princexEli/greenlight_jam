using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Wrapper
{
    float origVol;
    AudioSource source;

    public Audio_Wrapper(AudioSource newAud, float volume)
    {
        origVol = newAud.volume;
        source = newAud;
        updateVol(volume);
    }

    public void updateVol(float val)
    {
        source.volume = origVol * val;
    }
}
