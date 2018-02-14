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

    bool m_Talking;
    string m_CurrentText;
    // Use this for initialization
	void Start ()
    {
        m_Sentences = new Queue<string>();
        m_Faces = new Queue<Sprite>();
        m_Talking = false;
	}

    public void StartDialogue(Dialogue dialogue)
    {
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

        DisplayNextSentence();

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
            return;
        }

        if (m_Sentences.Count == 0)
        {
            EndOfDialogue();
            return;
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
       
    }

    void EndOfDialogue()
    {
        Debug.Log("End of conversation");
    }
}
