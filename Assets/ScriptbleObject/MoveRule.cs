using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoveRule", menuName = "Move Rule", order = 51)]
public class MoveRule : Rule
{
    public enum TypeMoveRule
    {
        Standart,
        OneMove,
        DiagonalMove,
        ForwardAndBackMove,
        ForwardMove,
        FreeMove,
        KnightMove,
        QueenMove,
        StraightLinesMove
    }
    
    public Piece.TypePiece m_TypePiece;
    public TypeMoveRule m_TypeMoveRule;

    [HideInInspector] public RulesManager.TypeRule m_TypeRule = RulesManager.TypeRule.Move;

    public IMoveRule GetMoveRule()
    {
        switch (m_TypeMoveRule)
        {
            case TypeMoveRule.OneMove:
                return new OneMove();
            
            default:
                return null; 
        }
    }
}
