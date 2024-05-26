using UnityEngine;

public class selectObjects : MonoBehaviour
{
    public int selectId;
    public void choose()
    {
        GameObject.Find("GameController").GetComponent<GameController>().choose(selectId);
    }
}
