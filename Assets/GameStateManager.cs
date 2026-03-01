using UnityEngine;
using UnityEngine.Playables;

public class GameStateManager : MonoBehaviour
{
    public GameStates currentState;

    [SerializeField] private PlayableDirector entranceTimeline;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Dialogue dialogueManager;
    public static GameStateManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        }
    }

    public void OnEntranceFinished()
    {
        SetState(GameStates.IntroDialogue);
    }
    
    public void OnDialogueFinished()
    {
        SetState(GameStates.Combat);
    }


}