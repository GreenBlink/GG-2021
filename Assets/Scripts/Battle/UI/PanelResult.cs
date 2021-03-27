using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResult : MonoBehaviour
{
    [SerializeField] private Text m_textResult;

    public void Initiate(int idPlayer)
    {
        if (idPlayer == -1)
        {
            return;
        }
        
        gameObject.SetActive(true);
        m_textResult.text = idPlayer == 0 ? "Победили белые" : "Победили черные";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
