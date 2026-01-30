using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComplate += DeliveryManager_OnRecipeComplate;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        if(sender is Player player)
        {
            PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
        }
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        if(sender is CuttingCounter cuttingCounter)
        {
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        }
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeComplate(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
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
