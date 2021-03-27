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
    [SerializeField] private List<VictoryRule> m_VictoryRules;
    
    [Space][SerializeField] private RuleDescription m_prefabDescriptionRule;
    [SerializeField] private Transform m_containerDescriptionRule;

    private PiecesManager m_piecesManager;
    private MoveRule m_currentMoveRule;
    private VictoryRule m_currentVictoryRule;

    private RuleDescription m_descriptionRuleMove;
    private RuleDescription m_descriptionRuleVictory;

    public void Initiate(PiecesManager piecesManager)
    {
        m_piecesManager = piecesManager;
        SetMoveRule(m_moveRules[0]);
        SetVictoryRule(m_VictoryRules[0]);
    }
    
    public void GenerationMove()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > 0.5f)
        {
            GenerationMoveVictory();
        }
        else
        {
            GenerationMoveRule();
        }
    }

    public int GetIsWin()
    {
        int idWin = -1;
        
        switch (m_currentVictoryRule.m_TypeVictoryRule)
        {
            case VictoryRule.TypeVictoryRule.KillKing:
                idWin = m_piecesManager.GetIsKillKing();
                break;
            
            case VictoryRule.TypeVictoryRule.KillPawn:
                idWin = m_piecesManager.GetIsKillPawn();
                break;
            
            case VictoryRule.TypeVictoryRule.KillAll:
                idWin = m_piecesManager.GetIsKillAll();
                break;
        }

        if (idWin == -1)
        {
            idWin = m_piecesManager.GetIsKillAll();
        }

        return idWin;
    }
    
    private void GenerationMoveVictory()
    {
        SetVictoryRule(m_VictoryRules[Random.Range(0, m_VictoryRules.Count)]);
    }

    private void GenerationMoveRule()
    {
        SetMoveRule(m_moveRules[Random.Range(0, m_moveRules.Count)]);
    }

    private void SetVictoryRule(VictoryRule victoryRule)
    {
        m_currentVictoryRule = victoryRule;
        m_descriptionRuleVictory = InitiateDescriptionRule(m_descriptionRuleVictory, victoryRule);
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

        m_descriptionRuleMove = InitiateDescriptionRule(m_descriptionRuleMove, moveRule);
    }

    private RuleDescription InitiateDescriptionRule(RuleDescription description, Rule rule)
    {
        if (description != null)
        {
            Destroy(description.gameObject);
        }

        description = Instantiate(m_prefabDescriptionRule, m_containerDescriptionRule);
        description.Initiate(rule);
        return description;
    }
}
