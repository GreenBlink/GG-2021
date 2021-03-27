using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChessboardSquare : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_frameBack;
    [SerializeField] private SpriteRenderer m_frameForward;
    [SerializeField] private Color m_colorClear;
    [SerializeField] private Color m_colorSelfPiece;
    [SerializeField] private Color m_colorEnemyPiece;

    private PiecesManager m_piecesManager;
    private Piece m_currentPiece;

    public void Initiate(PiecesManager piecesManager)
    {
        m_piecesManager = piecesManager;
        
        m_frameBack.DOFade(0, 0);
        m_frameForward.DOFade(0, 0);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_piecesManager.MoveParty(this, m_currentPiece);
        }
    }

    private void OnMouseEnter()
    {
        Light();
    }

    private void OnMouseExit()
    {
        Fade();
    }

    public void Light()
    {
        InitiateColorFrame();
        m_frameBack.DOFade(1, 0.5f);
        m_frameForward.DOFade(1, 0.5f);
    }

    public void Fade()
    {
        m_frameBack.DOFade(0, 0.5f);
        m_frameForward.DOFade(0, 0.5f);
    }

    public void SetPiece(Piece piece)
    {
        m_currentPiece = piece;
    }
    
    public void Clear()
    {
        m_currentPiece = null;
    }

    public bool IsClear() => m_currentPiece == null;

    public void Blink()
    {
        m_frameBack.color = m_colorEnemyPiece;
        m_frameForward.color = m_colorEnemyPiece;
        m_frameBack.DOFade(0, 0.2f).OnComplete(() => m_frameBack.DOFade(1, 0.2f));
        m_frameForward.DOFade(0, 0.2f).OnComplete(() => m_frameForward.DOFade(1, 0.2f));
    }

    public void DestroyPiece()
    {
        if (m_currentPiece != null)
        {
            m_currentPiece.Destroy();
            m_currentPiece = null;
        }
    }

    private void InitiateColorFrame()
    {
        if (m_currentPiece == null)
        {
            m_frameBack.color = m_colorClear;
            m_frameForward.color = m_colorClear;
        }
        else if (m_currentPiece.GetIdPlayer() == m_piecesManager.GetIdPlayer())
        {
            m_frameBack.color = m_colorSelfPiece;
            m_frameForward.color = m_colorSelfPiece;
        }
        else
        {
            m_frameBack.color = m_colorEnemyPiece;
            m_frameForward.color = m_colorEnemyPiece;
        }
    }
}
