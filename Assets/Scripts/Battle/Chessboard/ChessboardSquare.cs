using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChessboardSquare : MonoBehaviour
{
    public enum TypeLight
    {
        Target,
        Error,
        MouseOver, 
        Path
    }
    
    [SerializeField] private SpriteRenderer m_frameBack;
    [SerializeField] private SpriteRenderer m_frameForward;
    [SerializeField] private Color m_colorGray;
    [SerializeField] private Color m_colorGreen;
    [SerializeField] private Color m_colorRed;
    [SerializeField] private Color m_colorBlue;

    private PiecesManager m_piecesManager;
    private Piece m_currentPiece;
    private TypeLight m_typeLight;

    public void Initiate(PiecesManager piecesManager)
    {
        m_piecesManager = piecesManager;
        m_currentPiece = null;
        
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
        Light(TypeLight.MouseOver);
    }

    private void OnMouseExit()
    {
        Hide(false);
    }
    
    public void Hide(bool ignoreType)
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

    public bool IsThisPlayer(int idPlayer) => m_currentPiece != null && m_currentPiece.GetIdPlayer() == idPlayer;
    public bool IsClear() => m_currentPiece == null;

    public void DestroyPiece()
    {
        if (m_currentPiece != null)
        {
            m_currentPiece.Destroy();
            m_currentPiece = null;
        }
    }

    public void Light(TypeLight typeLight)
    {
        m_typeLight = typeLight;
        
        switch (typeLight)
        {
            case TypeLight.Target:
                SetColor(m_colorGreen);
                InitiateLight();
                break;
            
            case TypeLight.MouseOver:
                if (m_currentPiece == null || m_currentPiece.GetIdPlayer() != m_piecesManager.GetIdPlayer())
                {
                    SetColor(m_colorGray);
                }
                else
                {
                    SetColor(m_colorGreen);
                }
                
                InitiateLight();
                break;
            
            case TypeLight.Path:
                SetColor(m_colorBlue);
                InitiateLight();
                break;
            
            case TypeLight.Error:
                Blink();
                break;
        }
    }
    
    private void Blink()
    {
        Color color = m_frameBack.color;
            
        m_frameBack.DOColor(m_colorRed, 0.2f).OnComplete(() => m_frameBack.DOColor(color, 0.2f));
        m_frameForward.DOColor(m_colorRed, 0.2f).OnComplete(() => m_frameForward.DOColor(color, 0.2f));
    }
    
    private void InitiateLight()
    {
        m_frameBack.DOFade(1, 0.5f);
        m_frameForward.DOFade(1, 0.5f);
    }

    private void SetColor(Color color)
    {
        m_frameBack.color = color;
        m_frameForward.color = color;
    }
}
