using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Dialogue m_Dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_Dialogue);
    }

    public void TriggerFinalDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_Dialogue);
    }
}
