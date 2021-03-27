using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FreeMove : IMoveRule
{
	public bool Move(Transform transform, Chessboard chessboard, ChessboardSquare startSquare, ChessboardSquare targetSquare, int idPlayer)
	{
		transform.DOMove(targetSquare.transform.position, 0.5f);
		return true;
	}
	
	public int[] GetMassMove(int idPlayer)
	{
		return new[] {0};
	}
	
	public int GetMaxCountSquare()
	{
		return -1;
	}
}
