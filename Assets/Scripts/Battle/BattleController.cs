using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PiecesManager m_piecesManager;
    [SerializeField] private RulesManager m_rulesManager;
    [SerializeField] private PanelResult m_panelRezult;

    private float CHANCE_NEW_RULE = 0.2f;

    private void Start()
    {
        Restart();
        m_piecesManager.m_changePlayerEvent.AddListener(CheakWin);
    }

    public void Restart()
    {
        m_panelRezult.Hide();
        m_piecesManager.Initiate();
        m_rulesManager.Initiate(m_piecesManager);
    }

    public void ExitMenu()
    {
        
    }

    private void CheakWin()
    {
        m_panelRezult.Initiate(m_rulesManager.GetIsWin());
        NewRule();
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
