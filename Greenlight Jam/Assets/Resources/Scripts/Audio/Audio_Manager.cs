using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    Game_Manager manager;
    AudioSource[] musics;
    int currMusic = 1;

    AudioMixer mixer;
    AudioClip indoor_theme, hive_theme, map_theme, menu_theme, summary_theme;
    private static float volume = 10;

    private void Awake()
    {
        manager = Game_Manager.Instance;
        mixer = Resources.Load("Sound/MusicMixer") as AudioMixer;
        getThemes();
        setupMusic();
    }

    private void getThemes()
    {
        hive_theme = Resources.Load("Sound/Hive_Theme") as AudioClip;
        map_theme = Resources.Load("Sound/Map_Theme") as AudioClip;
        menu_theme = Resources.Load("Sound/Menu_Theme") as AudioClip;
        summary_theme = Resources.Load("Sound/Menu_Theme") as AudioClip;
        indoor_theme = Resources.Load("Sound/Map_Theme") as AudioClip;
    }

    private void setupMusic()
    {
        mixer = Resources.Load("Sound/MusicMixer") as AudioMixer;
       
        GameObject temp =new GameObject("Music");
        temp.transform.parent = gameObject.transform;
        
        musics = new AudioSource[2];
        musics[0] = temp.AddComponent<AudioSource>();
        musics[0].outputAudioMixerGroup = mixer.FindMatchingGroups("Music1")[0];
        musics[1] = temp.AddComponent<AudioSource>();
        musics[1].outputAudioMixerGroup = mixer.FindMatchingGroups("Music2")[0];
        
        swapTheme();
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
            case Helper.MENU:
                return menu_theme;
            case Helper.HIVE:
                return hive_theme;
            case Helper.SUMMARY:
                return summary_theme;
            case Helper.MAP:
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
            if (musics[1].clip == music) return;
            musics[1].clip = music;
            vol1 = -80;
            currMusic = 1;
		}
		else
		{
            if (musics[0].clip == music) return;
            musics[0].clip = music;
            vol2 = -80;
            currMusic = 0;
		}

        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music1Volume", manager.transitionDuration, vol1));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Music2Volume", manager.transitionDuration, vol2));
        musics[0].Play();
        musics[1].Play();
    }
}
