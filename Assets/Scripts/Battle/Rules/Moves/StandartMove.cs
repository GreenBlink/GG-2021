using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StandartMove : IMoveRule
{
    public bool Move(Transform transform, Chessboard chessboard, ChessboardSquare startSquare, ChessboardSquare targetSquare, int idPlayer)
    {
        List<ChessboardSquare> acceptSquares = chessboard.GetAcceptSquares(startSquare, GetMassMove(idPlayer), GetMaxCountSquare(), idPlayer);

        if (acceptSquares.Contains(targetSquare))
        {
            transform.DOMove(targetSquare.transform.position, 0.5f);
            return true;
        }

        return false;
    }

    public virtual int[] GetMassMove(int idPlayer)
    {
        return new[] {0};
    }
	
    public virtual int GetMaxCountSquare()
    {
        return -1;
    }
}
