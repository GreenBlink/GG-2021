using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PiecesManager m_piecesManager;
    [SerializeField] private RulesManager m_rulesManager;
    [SerializeField] private AIController m_aiController;
    [SerializeField] private PanelResult m_panelRezult;
    [SerializeField] private GameObject m_buttonFideAI;

    private float CHANCE_NEW_RULE = 0.2f;

    private void Start()
    {
        Restart();
        m_piecesManager.m_changePlayerEvent.AddListener(CheakMove);
    }

    public void Restart()
    {
        m_panelRezult.Hide();
        m_piecesManager.Initiate(m_aiController);
        m_rulesManager.Initiate(m_piecesManager);
        m_aiController.Initiate(m_piecesManager);
    }
    
    public void ControllerAI()
    {
        m_buttonFideAI.SetActive(!m_aiController.ControllerAI());
    }

    public void ExitMenu()
    {
        Application.Quit();
    }

    private void CheakMove()
    {
        CheakWin();
        NewRule();
    }

    private void CheakWin()
    {
        m_panelRezult.Initiate(m_rulesManager.GetIsWin());
    }

    private void NewRule()
    {
        float chance = Random.Range(0f, 1f);

        if (chance < CHANCE_NEW_RULE)
        {
            m_rulesManager.GenerationMove();
        }
    }
}
