using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
  private AudioSource A_SourceSFX;
  private AudioSource[] A_SourceMainThemeArray;

  public AudioClip[] soundEffects;
  public AudioClip[] mainTheme;
  private static AudioManagerScript _instance;

  public Dictionary<string, AudioClip>SoundEffectsDictionary;

  public GameManagerBehaviour gameManagerBehaviour;

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

    for (int i = 0; i < 9; i++)
    {
      A_SourceMainThemeArray[i] = gameObject.AddComponent<AudioSource>();
    }

    SoundEffectsDictionary = new Dictionary<string, AudioClip>();
    AddSoundEffectsToDictionary();

    PlayInteractiveThemeSilentInBackground();
   // PlayMainTheme("NotInteractive", 0); // REMOVE WHEN INTERACTIVE SOUNDTRACK
  }

  private void PlayInteractiveThemeSilentInBackground()
  {
    for (int i = 0; i < 9; i++) //: light 1-3; medium 4-6; high 7-9
    {
      PlayMainTheme(i);
      A_SourceMainThemeArray[i].volume = 0f;
    }
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

  public void PlayMainTheme(int audioSourceIndex)
  {
    if (!A_SourceMainThemeArray[audioSourceIndex].isPlaying)
    {
      A_SourceMainThemeArray[audioSourceIndex].loop = true;
      A_SourceMainThemeArray[audioSourceIndex].clip = mainTheme[audioSourceIndex];
      A_SourceMainThemeArray[audioSourceIndex].Play();
    }
  }
  public void SetVolumeMainThemeToMax(int audioSourceIndex)
  {
    A_SourceMainThemeArray[audioSourceIndex].volume = 1f;
  }


  // Update is called once per frame
  void Update()
  {
    
    //play light theme 
    if (gameManagerBehaviour.GetPollution() < 50)
    {
      // do shit so it plays after X seconds
    }
    else if   //play med theme theme 
    (gameManagerBehaviour.GetPollution() < 80)
      {
      // do shit so it plays after X seconds
      }
    else if   //play high theme theme 
 (gameManagerBehaviour.GetPollution() < 100)
    {
      // do shit so it plays after X seconds
    }



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

