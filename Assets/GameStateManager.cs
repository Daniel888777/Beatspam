using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameStates currentState;

    [SerializeField] private PlayableDirector entranceTimeline;
    [SerializeField] private PlayableDirector countDownTimeline;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Dialogue dialogueManager;
    [SerializeField] private string nextSceneName;
    public static GameStateManager Instance { get; private set; }

    void Awake()
    {
            Instance = this;
    }

    void Start()
    {
        SetState(GameStates.EntranceSequence);
    }

    public void SetState(GameStates newState)
    {
        currentState = newState;

        switch (currentState)
        {            
            case GameStates.EntranceSequence:
                entranceTimeline.Play();
                break;
            
            case GameStates.IntroDialogue:
                dialogueManager.Activate();
                break;
            
            case GameStates.Combat:
                audioManager.Play("Beat");
                dialogueManager.Deactivate();
                break;

            case GameStates.Victory:
                StartCoroutine(LoadNextScene());
                break;
        }
    }

    public void OnEntranceFinished()
    {
        SetState(GameStates.IntroDialogue);
    }
    
    public void OnDialogueFinished()
    {
        countDownTimeline.Play();
        //SetState(GameStates.Combat);
    }

    public void OnCountDownFinished()
    {
        SetState(GameStates.Combat);
    }

    public void OnPlayerVictory()
    {
        SetState(GameStates.Victory);
    }

    //--------------------------------//
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }
}