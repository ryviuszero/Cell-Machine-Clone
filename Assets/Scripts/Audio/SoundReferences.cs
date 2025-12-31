using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundReferences : ScriptableObject
{
    public List<Sound> sounds;

    public List<SoundGroup> soundGroups;

    public Dictionary<string, Sound> soundDict;

    public Dictionary<string, SoundGroup> soundGroupDict;

    private void OnEnable()
    {
        soundDict = new Dictionary<string, Sound>();
        foreach(Sound sound in sounds)
        {
            if (sound!=null)
            {
                soundDict[sound.name] = sound;
            }
        }
        soundGroupDict = new Dictionary<string, SoundGroup>();
        foreach(SoundGroup soundGroup in soundGroups)
        {
            if (soundGroup!=null)
            {
                soundGroupDict[soundGroup.name] = soundGroup;
            }
        }
    }
}