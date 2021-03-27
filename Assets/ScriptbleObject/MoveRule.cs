using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveRule", menuName = "Move Rule", order = 51)]
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
            
            case TypeMoveRule.DiagonalMove:
                return new DiagonalMove();
            
            case TypeMoveRule.ForwardAndBackMove:
                return new ForwardAndBackMove();
            
            case TypeMoveRule.ForwardMove:
                return new ForwardMove();
            
            case TypeMoveRule.FreeMove:
                return new FreeMove();
            
            case TypeMoveRule.KnightMove:
                return new KnightMove();
            
            case TypeMoveRule.QueenMove:
                return new QueenMove();
            
            case TypeMoveRule.StraightLinesMove:
                return new StraightLinesMove();
            
            default:
                return null; 
        }
    }
}
