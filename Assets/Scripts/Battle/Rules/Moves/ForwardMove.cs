using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ForwardMove : StandartMove
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

	public override int[] GetMassMove(int idPlayer)
	{
		return idPlayer == 0 ? new[] {8, 0} : new[] {-8, 0};
	}
	
	public override int GetMaxCountSquare()
	{
		return 1;
	}
}
