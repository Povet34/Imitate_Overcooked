using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

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
    float gamePlayingTimer = 60f;

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
                    }
                }
                break;
            case State.CountdownToStart:
                {
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer < 0)
                    {
                        state = State.GamePlaying;
                    }
                }
                break;
            case State.GamePlaying:
                {
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer < 0)
                    {
                        state = State.GameOver;
                    }
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
