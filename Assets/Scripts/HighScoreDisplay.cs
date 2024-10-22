using UnityEngine;
using TMPro;
public class HighScoreDisplay : MonoBehaviour
{

    private TMP_Text tmp_Text;

    public TMP_Text nameText;
    public TMP_Text scoreText;

    private void Awake()
    {
        tmp_Text = GetComponent<TMP_Text>();
    }

    public void DisplayHighScore(string name, int score)
    {
        nameText.text = name;
        scoreText.text = string.Format("{0:000000}", score);
    }

    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }

}
