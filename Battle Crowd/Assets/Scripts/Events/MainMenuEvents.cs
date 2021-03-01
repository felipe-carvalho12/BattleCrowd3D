using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Level");
    }
}
