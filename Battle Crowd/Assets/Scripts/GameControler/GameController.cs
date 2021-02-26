using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject levelCompleteCanvas;
    [SerializeField] Text armyCountText;
    static public int armyCount = 1;
    static public bool levelComplete;

    private void Start() {
        armyCount = 1;
        Time.timeScale = 1;
        levelComplete = false;
    }
    private void Update() {
        if (armyCount <= 0)
        {
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }
        if (levelComplete)
        {
            Time.timeScale = 0;
            levelCompleteCanvas.SetActive(true);
        }
        armyCountText.text = $"{armyCount} warrior{(armyCount != 1 ? "s" : "")}";
    }
}
