using UnityEngine;
using System.Xml.Serialization;

public class HighScoreEntry
{
    public int Score;
    public string Nickname;

    public HighScoreEntry() { } // Parameterless constructor for XML serialisation.

    public HighScoreEntry(int score, string nickname) // To save the player's score and nickname.
    {
        Score = score;
        Nickname = nickname;
    }
}