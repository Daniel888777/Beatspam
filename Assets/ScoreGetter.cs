using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using TMPro;

public class ScoreGetter : MonoBehaviour
{
    public static ScoreGetter Instance;
    private string baseUrl = "http://localhost/unity_api";
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void DisPlayScores()
    {
        StartCoroutine(ScoreFetcher());
    }


    private IEnumerator ScoreFetcher()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + "/get_scores.php"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) Debug.LogError(request.error);
            else Debug.Log("Scores: " + request.downloadHandler.text);
            scoreText.text = request.downloadHandler.text;
        }
    }


}



