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
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject quitButton;
    public GameObject instrucButton;
    public GameObject logoGame;
    private bool isPaused = false;
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
                quitButton.SetActive(false);
                instrucButton.SetActive(false);
                logoGame.SetActive(false);
                pauseButton.SetActive(true);
                break;
            case GameManagerState.Gameplay:
                ScoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                quitButton.SetActive(false);
                instrucButton.SetActive(false);
                logoGame.SetActive(false);
                playerShip.GetComponent<PlayerController>().Init();
                heartSpawner.GetComponent<HeartSpawer>().ScheduleHeartSpawner();
                enemySpawner.GetComponent<EnemySpawerController>().ScheduleEnemySpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                pauseButton.SetActive(true);
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

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
            resumeButton.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (resumeButton != null)
        {
            pauseButton.SetActive(true);
            resumeButton.SetActive(false);
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
