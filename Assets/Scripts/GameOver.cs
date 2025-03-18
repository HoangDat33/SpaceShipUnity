using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void noClick()
    {
        SceneManager.LoadScene(0);
    }
    public void yesClick()
    {
        SceneManager.LoadScene(1);
    }
}
