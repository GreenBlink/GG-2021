using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public enum TypeRule
    {
        Move,
        Destroy,
        Victory
    }

    [SerializeField] private List<MoveRule> m_moveRules;
    [SerializeField] private List<MoveRule> m_VictoryRules;
    
    [Space][SerializeField] private RuleDescription m_prefabDescriptionRule;
    [SerializeField] private Transform m_containerDescriptionRule;

    private PiecesManager m_piecesManager;
    private MoveRule m_currentMoveRule;

    private RuleDescription m_descriptionRuleMove;

    public void Initiate(PiecesManager piecesManager)
    {
        m_piecesManager = piecesManager;
        SetMoveRule(m_moveRules[0]);
    }

    public void GenerationMoveRule()
    {
        SetMoveRule(m_moveRules[Random.Range(0, m_moveRules.Count)]);
    }

    private void SetMoveRule(MoveRule moveRule)
    {
        m_currentMoveRule = moveRule;

        switch (m_currentMoveRule.m_TypeMoveRule)
        {
            case MoveRule.TypeMoveRule.Standart:
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.Castle, new StraightLinesMove());
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.Knight, new KnightMove());
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.Bishop, new DiagonalMove());
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.King, new OneMove());
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.Queen, new QueenMove());
                m_piecesManager.SetNewMoveRule(Piece.TypePiece.Pawn, new ForwardMove());
                break;
            
            default:
                m_piecesManager.SetNewMoveRule(m_currentMoveRule.m_TypePiece, m_currentMoveRule.GetMoveRule());
                break;
        }

        if (m_descriptionRuleMove != null)
        {
            Destroy(m_descriptionRuleMove.gameObject);
        }

        m_descriptionRuleMove = Instantiate(m_prefabDescriptionRule, m_containerDescriptionRule);
        m_descriptionRuleMove.Initiate(moveRule);
    }
}
