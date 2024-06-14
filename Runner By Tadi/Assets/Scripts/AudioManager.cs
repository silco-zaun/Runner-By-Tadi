using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Sound[] sounds;

    private new void Awake()
    {
        base.Awake();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            //s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play();
        }
    }
    public void PlaySound(string name, Vector3 position)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                AudioSource.PlayClipAtPoint(s.clip, position);
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                return s.clip;
        }

        return null;
    }
}
