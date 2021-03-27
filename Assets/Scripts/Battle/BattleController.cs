using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PiecesManager m_piecesManager;
    [SerializeField] private RulesManager m_rulesManager;

    private float CHANCE_NEW_RULE = 0.2f;

    private void Start()
    {
        Restart();
        m_piecesManager.m_changePlayerEvent.AddListener(NewRule);
    }

    public void Restart()
    {
        m_piecesManager.Initiate();
        m_rulesManager.Initiate(m_piecesManager);
        

    }

    public void ExitMenu()
    {
        
    }

    private void NewRule()
    {
        float chance = Random.Range(0f, 1f);

        if (chance < CHANCE_NEW_RULE)
        {
            m_rulesManager.GenerationMoveRule();
        }
    }
}
