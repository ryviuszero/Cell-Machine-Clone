using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundGroup : ScriptableObject
{
    public new string name;
    public List<Sound> sounds;

    public Sound GetSound()
    {
        int index = Random.Range(0, sounds.Count);
        return sounds[index];
    }

}