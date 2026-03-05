using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}

public class HighScorePoster : MonoBehaviour
{
    public static HighScorePoster Instance;
    private string baseUrl = "http://localhost/unity_api";

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
           Destroy(gameObject); 
           return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(ScoreData score)
    {
        StartCoroutine(PostScore(score));
    }

    public void GetScores()
    {
        StartCoroutine(GetScoreList());
    }

    private IEnumerator PostScore(ScoreData score)
    {
        string json = JsonUtility.ToJson(score);
        using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/add_score.php", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) Debug.LogError(request.error);
            else Debug.Log("Score Added: " + request.downloadHandler.text);
        }
    }

    private IEnumerator GetScoreList()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + "/get_scores.php"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) Debug.LogError(request.error);
            else Debug.Log("Scores: " + request.downloadHandler.text);
        }
    }
}
