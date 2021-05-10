using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.GetClip();

            s.source.volume = s.GetVolume();
            s.source.pitch = s.GetPitch();
            s.source.loop = s.IsLoop();
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.GetName() == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " isn't found");
            return;
        }
        s.GetSource().Play();
    }
}
