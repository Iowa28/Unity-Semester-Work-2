using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    
    [Range(0f, 1f)]
    [SerializeField] private float volume;
    [Range(.1f, 3f)]
    [SerializeField] private float pitch;

    [SerializeField] private bool loop;

    [HideInInspector] public AudioSource source;
    
    # region [ Getters ]
    public string GetName()
    {
        return name;
    }

    public AudioClip GetClip()
    {
        return clip;
    }
    
    public float GetVolume()
    {
        return volume;
    }
    
    public float GetPitch()
    {
        return pitch;
    }
    
    public AudioSource GetSource()
    {
        return source;
    }

    public bool IsLoop()
    {
        return loop;
    }
    # endregion
}
