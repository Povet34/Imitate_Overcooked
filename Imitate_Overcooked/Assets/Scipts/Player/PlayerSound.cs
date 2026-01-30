using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    Player player;
    float footStepTimer = 0f;
    float footStepTimerMax = 0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HandleFootSteps();
    }

    private void HandleFootSteps()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsMoving())
                SoundManager.Instance.PlayFootstepSound(player.transform.position);
        }
    }
}
