using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    AudioSource music;

    public AudioMixer mixer;
    AudioClip indoor_theme, hive_theme, map_theme, menu_theme, summary_theme;
    Slider masterSlider, musicSlider, effectsSlider;
    Dictionary<string, float> volumes;

    private void Awake()
    {
        mixer = Resources.Load("Sound/MusicMixer") as AudioMixer;
        setupVolumes();
        getThemes();
        setupMusic();
    }

    private void setupVolumes()
	{
        volumes = new Dictionary<string, float>();
        volumes.Add("Master", 1);
        volumes.Add("Music", 1);
        volumes.Add("Effects", 1);
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
        music = new AudioSource();

        GameObject temp =new GameObject("Music");
        temp.transform.parent = gameObject.transform;
        music = temp.AddComponent<AudioSource>();
        music.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
    }

    private void setupSliders()
	{
        masterSlider = GameObject.Find("Master Slider").gameObject.GetComponentInChildren<Slider>();
        masterSlider.value = volumes["Master"];
        masterSlider.onValueChanged.AddListener(delegate { updateVolume(masterSlider.value, "Master"); });
        musicSlider = GameObject.Find("Music Slider").gameObject.GetComponentInChildren<Slider>();
        musicSlider.value = volumes["Music"];
        musicSlider.onValueChanged.AddListener(delegate { updateVolume(musicSlider.value, "Music"); });
        effectsSlider = GameObject.Find("Effects Slider").gameObject.GetComponentInChildren<Slider>();
        effectsSlider.value = volumes["Effects"];
        effectsSlider.onValueChanged.AddListener(delegate { updateVolume(effectsSlider.value, "Effects"); });
    }

    public void Load()
	{
        string type = Helper.SceneType();
        if (type == Helper.MENU || type == Helper.PAUSE || type == Helper.MAP)
        {
            setupSliders();
        }
        swapTheme();
    }

    public float Volume(float val)
	{
        return Mathf.Log10(val) * 20;
    }

    public void updateVolume(float val, string mixerGroup)
    {
        volumes[mixerGroup] = val;
        float vol = Volume(val);
        mixer.SetFloat(mixerGroup, vol);
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
        AudioClip clip = getSceneMusic();
        if (music == clip) return;

        float vol = Volume(volumes["Music"]);
        music.clip = clip;
        mixer.SetFloat("Music", vol);
        music.Play();
    }
}
