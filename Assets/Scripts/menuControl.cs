using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class menuControl : MonoBehaviour
{
    public TextMeshProUGUI skorText;
    private void Start()
    {
        skorText.text = "En yüksek Skor: " + PlayerPrefs.GetInt("highScore").ToString();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
