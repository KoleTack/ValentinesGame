using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Queue<string> m_Sentences;
    public Queue<Sprite> m_Faces;
    public Image m_Face;

    public Text m_NameText;
    public Text m_DialogueText;

    public Trigger m_OnStartTrigger;

    public GameObject[] m_Response;
    public GameObject m_Continue;
    public GameObject m_EndScene;

    bool m_EndOfScene;
    bool m_EndGame = false;
    int m_SceneToLoad;
    bool m_Talking;
    string m_CurrentText;
    // Use this for initialization
	void Start ()
    {
        m_Continue = GameObject.FindGameObjectWithTag("Continue");
        m_Response = GameObject.FindGameObjectsWithTag("Response");
        m_EndScene = GameObject.FindGameObjectWithTag("EndScene");

        m_EndScene.SetActive(false);
        m_Sentences = new Queue<string>();
        m_Faces = new Queue<Sprite>();
        m_Talking = false;

        if (m_OnStartTrigger != null)
        {
            m_OnStartTrigger.TriggerDialogue();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Display continue button.

        Debug.Log("Starting conversation with " + dialogue.m_Name);
        Debug.Log(dialogue.m_Sentences.Length);

        m_Sentences.Clear();

        m_Face = dialogue.m_Face;

        foreach (string sentence in dialogue.m_Sentences)
        {
            m_Sentences.Enqueue(sentence);
        }

     
        foreach (Sprite sprite in dialogue.m_CurrentFace)
        {
            m_Faces.Enqueue(sprite);
        }
      
        m_NameText.text = dialogue.m_Name;
        m_EndOfScene = dialogue.m_EndScene;
        m_EndGame = dialogue.m_EndGame;
        m_SceneToLoad = dialogue.m_SceneToLoad;
        DisplayNextSentence();

    }

    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneToLoad);
    }

    public void DisplayNextSentence()
    {
        

        //check if still talking
        if (m_Talking)
        {
            //stop coroutine and finish sentence
            StopAllCoroutines();
            m_Talking = false;
            m_DialogueText.text = m_CurrentText;

            //check if this was last sentence
            if(m_Sentences.Count == 0)
            {
                EndOfDialogue();
            }
            return;
        }

        if (m_Sentences.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        m_Continue.SetActive(true);

        foreach (GameObject response in m_Response)
        {
            response.SetActive(false);
        }

        m_Face.sprite = m_Faces.Dequeue();
        m_CurrentText = m_Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(m_CurrentText));       
    }

    IEnumerator TypeSentence(string sentence)
    {
        m_Talking = true;

        m_DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            m_DialogueText.text += letter;

            yield return null;
        }

        m_Talking = false;  
        if(m_Sentences.Count == 0)
        {
            //Display response options.
            EndOfDialogue();
        }
    }

    void EndOfDialogue()
    {
        if (m_EndGame)
        {
            m_Continue.SetActive(false);
            m_EndScene.SetActive(false);

            foreach (GameObject response in m_Response)
            {
                response.SetActive(false);
            }

            return;
        }


        m_Continue.SetActive(false);

        if(m_EndOfScene)
        {
            m_EndScene.SetActive(true);
            return;
        }

        foreach (GameObject response in m_Response)
        {
            response.SetActive(true);
        }

        Debug.Log("Display Responses");
    }
}
