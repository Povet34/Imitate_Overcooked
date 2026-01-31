using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    State state;
    float waitToStartTimer = 1f;
    float countdownToStartTimer = 3f;
    float gamePlayingTimer;
    float gamePlayingTimerMax = 60f;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                {
                    waitToStartTimer -= Time.deltaTime;
                    if (waitToStartTimer < 0)
                    {
                        state = State.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.CountdownToStart:
                {
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0)
                    {
                        state = State.GamePlaying;
                        gamePlayingTimer = gamePlayingTimerMax;

                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.GamePlaying:
                {
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0)
                    {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
