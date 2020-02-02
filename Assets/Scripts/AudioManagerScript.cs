using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
  private AudioSource A_SourceSFX;
  public AudioSource[] A_SourceMainThemeArray;

  public AudioClip[] soundEffects;
  public AudioClip[] mainTheme;
  private static AudioManagerScript _instance;

  public Dictionary<string, AudioClip>SoundEffectsDictionary;

  public GameManagerBehaviour gameManagerBehaviour;


  private float beatsPerMinute, beatsPerSecond, beatsPerTwoBars, secondsPerTwoBar, twoBarFixedSecond;

  public int lightPollutionTreshold, mediumPollutionTreshold, highPollutionTreshold;

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
    beatsPerMinute = 96; //a 16 bar loop with this bpm is a steady 10 seconds
    twoBarFixedSecond = 0;
    A_SourceSFX = gameObject.AddComponent<AudioSource>();

    //for (int i = 0; i < 9; i++)
    //{
    //  A_SourceMainThemeArray[i] = new AudioSource();
    //  A_SourceMainThemeArray[i]  = gameObject.AddComponent<AudioSource>();
    //}

    SoundEffectsDictionary = new Dictionary<string, AudioClip>();
    AddSoundEffectsToDictionary();

    PlayInteractiveThemeSilentInBackground();
   // PlayMainTheme("NotInteractive", 0); // REMOVE WHEN INTERACTIVE SOUNDTRACK
  }

  private void FixedUpdate() // called every 0.02
  {

    //10(16 bars is 10 seconds) * 50 = 500
    if (twoBarFixedSecond < 500)
    {
      twoBarFixedSecond += 1;
    }
    else if (twoBarFixedSecond == 500)
    {

      //check pollution level //TODO: make it so that you  know the previous level of pollution and know which one of the 9 to get

      //play light theme 
      if (gameManagerBehaviour.GetPollution() < lightPollutionTreshold)
      {

       //light theme is 0-2 
        SetVolumeMainThemeToMax(0);
      }
      else if   //play med theme theme 
      (gameManagerBehaviour.GetPollution() < mediumPollutionTreshold)
      {
        //medium theme is 3-5
        SetVolumeMainThemeToMax(3);
      }
      else if   //play high theme theme 
   (gameManagerBehaviour.GetPollution() < highPollutionTreshold)
      {
        //high theme is 6-8
        SetVolumeMainThemeToMax(6);
      }
      twoBarFixedSecond = 0;
    }
    else
    {
      twoBarFixedSecond = 0;
    }
    
   // Time.fixedTime;

  }
  private void PlayInteractiveThemeSilentInBackground()
  {
    for (int i = 0; i < 9; i++) //: light 1-3; medium 4-6; high 7-9
    {
      PlayMainTheme(i);
      A_SourceMainThemeArray[i].volume = 0f;
    }
    twoBarFixedSecond = 500;
  }

  public void PlaySoundEffects(string soundEffectName)
  {
    //Stop any playing music
    //A_Source.Stop();
    AudioClip audioClip;
    if(SoundEffectsDictionary.TryGetValue(soundEffectName, out audioClip))
    {
      A_SourceSFX.volume = 0.3f;
      A_SourceSFX.PlayOneShot(audioClip);
    }
  }

  public void PlayMainTheme(int audioSourceIndex)
  {
      A_SourceMainThemeArray[audioSourceIndex].loop = true;
      A_SourceMainThemeArray[audioSourceIndex].clip = mainTheme[audioSourceIndex];
      A_SourceMainThemeArray[audioSourceIndex].Play();  
  }
  public void SetVolumeMainThemeToMax(int audioSourceIndex)
  {
    for (int i = 0; i < A_SourceMainThemeArray.Length; i++)
    {
      A_SourceMainThemeArray[i].volume = 0f;
    }
    A_SourceMainThemeArray[audioSourceIndex].volume = 1f;
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

