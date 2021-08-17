using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup sfxAudioMixerGroup;
    public Sound[] sounds;
    public Sound[] footsteps;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = sfxAudioMixerGroup ;
        }
        foreach (Sound s in footsteps)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = sfxAudioMixerGroup;
        }
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("wrong name of the clip");
            return;
        }
        s.source.Play();
    }
    public void PlayReverseAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("wrong name of the clip");
            return;
        }
        s.source.timeSamples = s.source.clip.samples - 1;
        s.source.pitch = -1;
        s.source.Play();
    }
    public void PlayCharacterFootstep()
    {
        int rand = UnityEngine.Random.Range(0, footsteps.Length - 1);
        footsteps[rand].source.Play();
    }
}
