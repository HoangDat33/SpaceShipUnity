using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject heartSpawner;
    public GameObject GameOverGO;
    public GameObject ScoreUITextGO;
    public GameObject TimeCounterGO;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    private void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(false);
                break;
            case GameManagerState.Gameplay:
                ScoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                playerShip.GetComponent<PlayerController>().Init();
                heartSpawner.GetComponent<HeartSpawer>().ScheduleHeartSpawner();
                enemySpawner.GetComponent<EnemySpawerController>().ScheduleEnemySpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                break;
            case GameManagerState.GameOver:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                heartSpawner.GetComponent<HeartSpawer>().UnscheduleHeartSpawner();
                enemySpawner.GetComponent<EnemySpawerController>().UnscheduleEnemySpawner();
                GameOverGO.SetActive(true);
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        SetGameManagerState(GameManagerState.Gameplay);
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
