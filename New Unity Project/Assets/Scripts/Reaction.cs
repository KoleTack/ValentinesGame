using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reaction : MonoBehaviour {

    public Sprite m_Image;

    public void React()
    {
        if(m_Image != null)
        GameObject.FindGameObjectWithTag("KoleImage").GetComponent<Image>().sprite = m_Image;
    }
}
