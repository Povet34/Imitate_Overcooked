using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefsSO", menuName = "Scriptable Objects/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footStep;
    public AudioClip[] objectPickup;
    public AudioClip[] objectDrop;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip stoveSizzle;
}
