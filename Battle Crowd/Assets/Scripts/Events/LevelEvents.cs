using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEvents : MonoBehaviour
{
    public void ReplayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
