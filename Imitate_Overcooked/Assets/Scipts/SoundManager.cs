using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComplate += DeliveryManager_OnRecipeComplate;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    private void DeliveryManager_OnRecipeComplate(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        int randomIndex = UnityEngine.Random.Range(0, audioClipArray.Length);
        AudioClip audioClip = audioClipArray[randomIndex];
        PlaySound(audioClip, position, volume);
    }

    void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
