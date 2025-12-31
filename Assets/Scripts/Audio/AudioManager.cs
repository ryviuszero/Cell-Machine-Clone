using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public SoundReferences sounds;

    private AudioSource musicSource;

    private string musicName;

    public void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }    
    }

    private AudioSource CreateAudioSourceGameObject(string soundName)
    {
        Sound sound = sounds.soundDict[soundName];
        return CreateAudioSourceGameObject(sound);
    }

    private AudioSource CreateAudioSourceGameObject(Sound sound)
    {
        AudioSource audioSource = new GameObject("SFX:" + sound.name).AddComponent<AudioSource>();
        audioSource.clip = sound.clip;
        audioSource.pitch = Random.Range(sound.minPitch, sound.maxPitch);
        audioSource.volume = sound.volume;
        return audioSource;
    }

    public AudioSource Play(Sound sound, float volume = 1f)
    {
        AudioSource audioSource = CreateAudioSourceGameObject(sound);
        audioSource.volume *= volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, sound.clip.length);
        return audioSource;
    }

    public AudioSource Play(string soundName, float volume = 1f)
    {
        Sound sound = sounds.soundDict[soundName];
        return Play(sound, volume);
    }

    public void PlaySoundGroup(string soundGroupName, float volume = 1f)
    {
        SoundGroup soundGroup = sounds.soundGroupDict[soundGroupName];
        Play(soundGroup.GetSound(), volume);
    }

    public void PlayMusic(string soundName)
    {
        if( musicName == null)
        {
            musicSource = CreateAudioSourceGameObject(soundName);
            musicSource.loop = true;
            musicSource.transform.SetParent(transform);
            musicName = soundName;
        }
        else if(soundName != musicName)
        {
            musicSource.clip = sounds.soundDict[soundName].clip;
            musicName = soundName;
        }
        if(!musicSource.isPlaying)
        {
            musicSource?.Play();
        }
    }

    public void PauseMusic()
    {
        musicSource?.Pause();
    }

}
