using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

	public AudioSource music1, music2;
    public float transitionDuration;
    bool use1 = true;
    public AudioMixer mixer;
    public static float maxDist;
    [Header("Background Music")]
    public AudioClip mainMenu_theme;
    public AudioClip hive_theme, map_theme, indoor_theme;

    private static float volume = 10;
    private static List<Audio_Wrapper> sources;

    private void Awake()
    {
        sources = new List<Audio_Wrapper>();
        addSource(music1);
        addSource(music2);
    }

    public Audio_Wrapper addSource(AudioSource source)
    {
        Audio_Wrapper temp = new Audio_Wrapper(source, volume, maxDist);
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


    private AudioClip getSceneMusic()
	{
        string scene = Helper.SceneType();
        if (scene == "Pause")
		{
            if(Helper.SceneName() == "Ground")
			{
                return map_theme;
			}
			else
			{
                return indoor_theme;
			}
		}
        else if (scene == "The Hive")
		{
            return hive_theme;
		}
        else if (scene == "Main Menu")
        {
            return mainMenu_theme;
        }
        else
		{
            Debug.LogError("No music for scene type " + scene + ". Will use main menu music instead.");
            return mainMenu_theme;
		}
	}

    public void swapTheme()
    {
        AudioClip music = getSceneMusic();
        float vol1 = volume, vol2 = volume;
		if (use1)
		{
            music2.clip = music;
            vol1 = 0;
            use1 = false;
		}
		else
		{
            music1.clip = music;
            vol2 = 0;
            use1 = true;
		}

        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", transitionDuration, vol1));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", transitionDuration, vol2));
    }
}
