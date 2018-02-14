using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counting : MonoBehaviour
{
    public int m_CurrentCherries = 2;
    public Text m_DialogueBox;

    public void IncreaseCherries()
    {
        m_CurrentCherries++;
        m_DialogueBox.text = "How about please with " + m_CurrentCherries.ToString() + " cherries on top?";
    }

	
}
