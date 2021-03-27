using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class QueenMove : IMoveRule
{
	public bool Move(Transform transform, Chessboard chessboard, ChessboardSquare startSquare, ChessboardSquare targetSquare)
	{
		List<ChessboardSquare> acceptSquares = chessboard.GetAcceptSquares(startSquare, GetMassMove(), GetMaxCountSquare());

		if (acceptSquares.Contains(targetSquare))
		{
			transform.DOMove(targetSquare.transform.position, 0.5f);
			return true;
		}

		return false;
	}

	public int[] GetMassMove()
	{
		return new[] {8, -8, 9, -9, 7, -7, 1, -1, 0};
	}
	
	public int GetMaxCountSquare()
	{
		return -1;
	}
}
