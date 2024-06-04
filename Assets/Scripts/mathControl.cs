using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class mathControl : MonoBehaviour
{
    public TextMeshProUGUI timerText, questionText;
    public TMP_InputField answerInput;
    private bool isAnswered;
    public float answerTimer = 15;
    private int answer;
    private void OnEnable()
    {
        int quest1, quest2;
        quest1 = Random.Range(1, 10);
        quest2 = Random.Range(1, 10);
        answer = quest1 + quest2;
        questionText.text = quest1 + " + " + quest2 + " = ";

    }
    void Update()
    {
        if (!isAnswered)
        {
            answerTimer -= Time.deltaTime;
            timerText.text = answerTimer.ToString("F2");
            if (answerTimer <= 0)
            {
                GameObject.FindObjectOfType<GameController>().failedObject.SetActive(true);
                isAnswered = true;
            }
        }
    }
    public void giveAnswer()
    {
        isAnswered = true;
        if (answerInput.text == answer.ToString())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            GameObject.FindObjectOfType<GameController>().failedObject.SetActive(true);
        }
    }
}
