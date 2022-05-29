using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    #region instance
    private static Audio_Manager instance;
    public static Audio_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Audio_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
	#endregion

	public AudioSource backgroundMusic, effects;
    public AudioClip save, recruit;

    private static float volume;
    private static List<Audio_Wrapper> sources;

    private void Awake()
    {
        sources = new List<Audio_Wrapper>();
        if(backgroundMusic != null)
		{
            addSource(backgroundMusic);
            backgroundMusic.spatialBlend = 0;
        }
        if(effects!= null)
		{
            addSource(effects);
            backgroundMusic.spatialBlend = 0;
        }
    }

    public Audio_Wrapper addSource(AudioSource source)
    {
        Audio_Wrapper temp = new Audio_Wrapper(source, volume);
        sources.Add(temp);
        return temp;
    }

    public void removeSource(Audio_Wrapper source)
    {
        sources.Remove(source);
    }

    public void updateVolume(float val)
    {
        volume = val;
        updateSources();
    }

    private void updateSources()
    {
        foreach(Audio_Wrapper source in sources)
        {
            source.updateVol(volume);
        }
    }
}
