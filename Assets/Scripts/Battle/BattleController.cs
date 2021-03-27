using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PiecesManager m_piecesManager;

    private void Start()
    {
        m_piecesManager.Initiate();
    }
}
