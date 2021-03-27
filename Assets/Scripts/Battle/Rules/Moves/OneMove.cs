using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OneMove : StandartMove
{
	public override int[] GetMassMove(int idPlayer)
	{
		return new[] {8, -8, 9, -9, 7, -7, 1, -1, 0};
	}
	
	public override int GetMaxCountSquare()
	{
		return 1;
	}
}
