using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DiagonalMove : StandartMove
{
	public override int[] GetMassMove(int idPlayer)
	{
		return new[] {9, 7, -9, -7, 0};
	}
	
	public override int GetMaxCountSquare()
	{
		return -1;
	}
}
