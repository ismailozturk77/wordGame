using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public string[] sentences;
    public static int lastSelected = -1;
    public static int skor = 0;
    private List<string> selectedSentences = new List<string>();
    private List<int> chooseOrder = new List<int>();
    private List<int> choosedOrder = new List<int>();
    public GameObject textParent, correctObject, failedObject, touchStop, questionObject;
    public TextMeshProUGUI answerText, skorText;
    public AudioSource clickSound;
    private void Start()
    {
        skorText.text = "Skor: " + skor.ToString();
        if (skor > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", skor);
        }
        selectRandomSentence();
        answerText.text = "Bekle";
    }
    public void selectRandomSentence()
    {
        while (true)
        {
            int selected = Random.Range(0, sentences.Length);
            if (selected != lastSelected)
            {
                lastSelected = selected;
                for (int i = 0; i < 4; i++)
                {
                    selectedSentences.Add(sentences[selected].Split(" ")[i]);
                }
                break;
            }
        }
        while (selectedSentences.Count < 16)
        {
            int selected = Random.Range(0, sentences.Length);
            if (selected != lastSelected)
            {
                int randomSelectedWord = Random.Range(0, 4);
                if (!selectedSentences.Contains(sentences[selected].Split(" ")[randomSelectedWord]))
                {
                    selectedSentences.Add(sentences[selected].Split(" ")[randomSelectedWord]);
                }
            }
        }
        selectedSentences = selectedSentences.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < 16; i++)
        {
            textParent.transform.GetChild(i).GetComponent<selectObjects>().selectId = i;
            textParent.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = selectedSentences[i];
        }
        for (int i = 0; i < 4; i++)
        {
            chooseOrder.Add(selectedSentences.IndexOf(sentences[lastSelected].Split(" ")[i]));
        }
        StartCoroutine(startShow());
    }
    IEnumerator startShow()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 4; i++)
        {
            textParent.transform.GetChild(chooseOrder[i]).GetComponentInChildren<Image>().color = Color.green;
            yield return new WaitForSeconds(1);
            textParent.transform.GetChild(chooseOrder[i]).GetComponentInChildren<Image>().color = Color.white;
        }
        answerText.text = "Git";
        touchStop.SetActive(false);
    }
    public void choose(int selectId)
    {
        if (answerText.text == "Git")
        {
            answerText.text = "";
        }
        clickSound.PlayOneShot(clickSound.clip);
        choosedOrder.Add(selectId);
        answerText.text += selectedSentences[selectId] + " ";
        if (choosedOrder.Count == 4)
        {
            if (choosedOrder.SequenceEqual(chooseOrder))
            {
                correctObject.SetActive(true);
                skor += 1;
            }
            else
            {
                questionObject.SetActive(true);
            }
        }
    }
    public void resetScene()
    {
        SceneManager.LoadScene(1);
    }
    public void anaMenu()
    {
        if (skor > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", skor);
        }
        skor = 0;
        SceneManager.LoadScene(0);
    }
}
