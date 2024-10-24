using UnityEngine;
using System.Xml.Serialization;

public class HighScoreEntry
{
    public int Score;
    public string Nickname;

    public HighScoreEntry() { } // Parameterl�s konstrukt�r f�r XML-serialisering

    public HighScoreEntry(int score, string nickname)
    {
        Score = score;
        Nickname = nickname;
    }
}