using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {


    public string m_Name;

    [TextArea(3, 10)]
    public string[] m_Sentences;

    public Image m_Face;

    public Sprite[] m_CurrentFace;

    



}
