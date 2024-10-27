using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public List<HighScoreEntry> highScores;
    private const string fileName = "highscores.xml";
    private const int maxScores = 5;
    public static HighScoreManager Instance { get; private set; }


    // Uses DontDestroyOnLoad to keep this object between scene changes.
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        // Ensure only one instance of HighScoreManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object between scenes.
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances.
        }
    }

    // Existing points are loaded from the file when the game starts.
    void Start()
    {
        LoadScores();
    }

    /// <summary>
    /// Method that loads high scores from an XML file.
    /// Check if the high scores file exists.
    /// Create a serialiser to read the list from XML format.
    /// Open the file and deserialise the content into a list of high scores.
    /// </summary>

    public void LoadScores()
    {

        if (File.Exists(GetFilePath()))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreEntry>));
            using (FileStream stream = new FileStream(GetFilePath(), FileMode.Open))
            {
                highScores = (List<HighScoreEntry>)serializer.Deserialize(stream);
            }
            Debug.Log("no list found");
        }
        else
        {
            highScores = new List<HighScoreEntry>();
        }
    }

    /// <summary>
    /// The following is a method to save the current high scores to an XML file.
    /// Create or overwrite the high scores file.
    /// Serialise (write) the list to the file.
    /// </summary>

    public void SaveScores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreEntry>));
        using (FileStream stream = new FileStream(GetFilePath(), FileMode.Create))
        {
            serializer.Serialize(stream, highScores);
        }
    }

    /// <summary>
    /// Method to add a new score and sort the list of high scores.
    /// Creates a new high score record with score and name. To then add the record to the list.
    /// Sort the list in descending order (highest score first).
    /// If the list exceeds the maximum number (5), delete the last record.
    /// Save the updated high scores to the file.
    /// </summary>

    public void AddScore(int newScore, string nickname)
    {
        HighScoreEntry newEntry = new HighScoreEntry(newScore, nickname);
        highScores.Add(newEntry);
        highScores.Sort((x, y) => y.Score.CompareTo(x.Score));

        if (highScores.Count > maxScores)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveScores();
    }

    /// <summary>
    /// Method to check if a new score qualifies as a high score.
    /// Checks if the list has less than maxScores or if the score is higher than the lowest high score.
    /// A check for whether the score qualifies or not.
    /// </summary>


    public bool IsHighScore(int score)
    {
        if (highScores.Count < maxScores || score > highScores[maxScores - 1].Score)
        {
            return true;
        }
        return false;
    }

    // Private method to retrieve the file path where high scores are saved.
    private string GetFilePath()
    {
        // Combines the game's persistent data folder and the file name to get the full path of the file.
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}