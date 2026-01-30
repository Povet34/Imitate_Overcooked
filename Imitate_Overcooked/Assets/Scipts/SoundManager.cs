using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComplate += DeliveryManager_OnRecipeComplate;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
        TrashCounter.OnTrashed += TrashCounter_OnTrashed;
    }

    private void TrashCounter_OnTrashed(object sender, EventArgs e)
    {
        if(sender is TrashCounter trashCounter)
        {
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        }
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, EventArgs e)
    {
        if(sender is BaseCounter counter)
        {
            PlaySound(audioClipRefsSO.objectDrop, counter.transform.position);
        }
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

    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }
}
