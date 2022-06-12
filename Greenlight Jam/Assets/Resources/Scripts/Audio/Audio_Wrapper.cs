using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Wrapper
{
    float origVol;
    AudioSource audioComp;

    public Audio_Wrapper(AudioSource newAud, float volume, float maxDist)
    {
        origVol = newAud.volume;
        audioComp = newAud;
        audioComp.maxDistance = maxDist;
        audioComp.minDistance = 1;
        audioComp.spatialBlend = 0.9f;
        updateVol(volume);
    }

    public void updateVol(float val)
    {
        audioComp.volume = origVol * val;
    }
}
