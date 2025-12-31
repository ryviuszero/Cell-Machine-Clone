using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public new string name;

    public AudioClip clip;

    [Range(0.1f, 2f)]
    public float minPitch = 1f;

    [Range(0.1f, 2f)]
    public float maxPitch = 1f;

    [Range(0f, 2f)]
    public float volume = 1f;

}