using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    void Start()
    {
        UpdateDisplay();
    }

    void Load()
    {
        scores = XMLManager.instance.LoadScores();
    }

    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }

    // Kolla om spelaren score kvalificerar
    public bool IsNewHighScore(int playerScore)
    {
        if (scores.Count < 5)
        {
            return true; // Om topplistan har färre än 5 deltagare är alla score kvalificerade.
        }

        // kolla ifall spelarens score är högre än femte plats
        return playerScore > scores[scores.Count - 1].score;
    }

    public void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        // Se till att topplistan bara har 5 platser
        if (scores.Count > 5)
        {
            scores.RemoveAt(scores.Count - 1);
        }

        UpdateDisplay();
    }

    public void Save(int playerScore)
    {
        // Kollar om IsNewHighScore ger resultatet om att det är ett nytt hichscore.
        if (IsNewHighScore(playerScore))
        {
            // Om spelarens score kvalificerar så sparas den.
            XMLManager.instance.SaveScores(scores);
        }
    }

}