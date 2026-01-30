using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    State state;
    float waitToStartTimer = 1f;

    private void Awake()
    {
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                waitToStartTimer -= Time.deltaTime;
                if (waitToStartTimer < 0)
                {
                    state = State.CountdownToStart;
                }

                state = State.CountdownToStart;
                break;
        }
    }
}
