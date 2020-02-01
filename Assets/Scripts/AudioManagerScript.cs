using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
  private AudioSource A_SourceSFX;
  private AudioSource A_SourceMainTheme;

  public AudioClip[] soundEffects;
  public AudioClip[] mainTheme;
  private static AudioManagerScript _instance;

  public Dictionary<string, AudioClip>SoundEffectsDictionary;

  public static AudioManagerScript Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = GameObject.FindObjectOfType<AudioManagerScript>();
      }

      return _instance;
    }
  }
  
  void Start()
    {
    A_SourceSFX = gameObject.AddComponent<AudioSource>();
    A_SourceMainTheme = gameObject.AddComponent<AudioSource>();
    SoundEffectsDictionary = new Dictionary<string, AudioClip>();

    AddSoundEffectsToDictionary();

    PlayMainTheme("NotInteractive", 0); // REMOVE WHEN INTERACTIVE SOUNDTRACK
  }

 

  public void PlaySoundEffects(string soundEffectName)
  {

    //Stop any playing music
    //A_Source.Stop();
    AudioClip audioClip;
    if(SoundEffectsDictionary.TryGetValue(soundEffectName, out audioClip))
    {
      A_SourceSFX.PlayOneShot(audioClip);
    }
  }

  public void PlayMainTheme(string pollutionlevel, int TrackID)
  {
    switch (pollutionlevel)
    {
      case "Light":
        A_SourceMainTheme.PlayOneShot(mainTheme[TrackID]);
        break;
      case "Medium":
        A_SourceMainTheme.PlayOneShot(mainTheme[TrackID]);
        break;
      case "High":
        A_SourceMainTheme.PlayOneShot(mainTheme[TrackID]);
        break;
      case "NotInteractive":
        A_SourceMainTheme.loop = true;
        A_SourceMainTheme.clip = mainTheme[TrackID];
        A_SourceMainTheme.Play();
        break;
      default:
    //    A_SourceMainTheme.PlayOneShot(Clip_00);
        break;
    }
  }


  // Update is called once per frame
  void Update()
    {
   
}

  private void AddSoundEffectsToDictionary()
  {
    int i = 0;
    //adding all soundeffects to the dictionary
    SoundEffectsDictionary.Add("plantTree", soundEffects[i++]);
    SoundEffectsDictionary.Add("factorySpawn", soundEffects[i++]);
    SoundEffectsDictionary.Add("treeTierUp", soundEffects[i++]);
    SoundEffectsDictionary.Add("factoryTierUp", soundEffects[i++]);
    SoundEffectsDictionary.Add("factoryDestroy", soundEffects[i++]);
    SoundEffectsDictionary.Add("invalidAction", soundEffects[i++]);
  }
}

