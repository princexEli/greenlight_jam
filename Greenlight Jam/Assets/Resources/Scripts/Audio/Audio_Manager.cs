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
    AudioSource[] musics;
    int currMusic = 1;

    public float transitionDuration;
    AudioMixer mixer;

    [Header("Background Music")]
    public AudioClip mainMenu_theme;
    public AudioClip hive_theme, map_theme, indoor_theme;

    private static float volume = 10;

    private void Awake()
    {
        mixer = Resources.Load("Sound/MusicMixer") as AudioMixer;
        setupMusic();
    }

    private void setupMusic()
    {
        mixer = Resources.Load("Sound/MusicMixer") as AudioMixer;
        musics = new AudioSource[2];
        musics[0] = gameObject.AddComponent<AudioSource>();
        musics[0].outputAudioMixerGroup = mixer.FindMatchingGroups("Music1")[0];
        musics[1] = gameObject.AddComponent<AudioSource>();
        musics[1].outputAudioMixerGroup = mixer.FindMatchingGroups("Music2")[0];
        
        swapTheme();
    }

    public AudioSource createSource()
    {
        AudioSource source = new AudioSource();
        return source;
    }


    public void updateVolume(float val)
    {
        volume = val;
        mixer.SetFloat("MasterVolume", val);
    }


    private AudioClip getSceneMusic()
	{
        switch (Helper.SceneType())
        {
            case "Menu":
                return mainMenu_theme;
            case "Hive":
                return hive_theme;
            case "Summary":
                return hive_theme;
            case "Ground":
                return map_theme;
            default:
                return indoor_theme;
        }
	}

    public void swapTheme()
    {
        AudioClip music = getSceneMusic();
        float vol1 = volume, vol2 = volume;
		if (currMusic==0)
		{
            musics[1].clip = music;
            vol1 = -80;
            currMusic = 1;
		}
		else
		{
            musics[0].clip = music;
            vol2 = -80;
            currMusic = 0;
		}

        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", transitionDuration, vol1));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", transitionDuration, vol2));
        musics[0].Play();
        musics[1].Play();
    }
}
