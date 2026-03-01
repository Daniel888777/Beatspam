using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] sentences;
    [SerializeField] private float typingSpeed = 0.05f;
    private int index;
    private bool closing = false;
    [SerializeField] private UnityEngine.UI.Image playerImage;
    [SerializeField] private UnityEngine.UI.Image enemyImage;
    
    
    void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerImage.gameObject.SetActive(true);
        enemyImage.gameObject.SetActive(false);
        dialogueText.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == sentences[index])
            {
                NextSentence();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = sentences[index];
            }
        }

        if (index == sentences.Length - 1 && dialogueText.text == sentences[index])
        {
            closing = true;
            StartCoroutine(CloseDialogue());
        }

    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(Type());
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Type());
            
            if (index == 3)
            {
                dialogueText.color = Color.red;
                playerImage.gameObject.SetActive(false);
                enemyImage.gameObject.SetActive(true);
            }
        
        }

       
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator CloseDialogue()
    {
        yield return new WaitForSeconds(2f);
        //playerImage.gameObject.SetActive(false);
        //enemyImage.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        GameStateManager.Instance.SetState(GameStates.Combat);
    }



}
