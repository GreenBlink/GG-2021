using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KnightMove : StandartMove
{
	public override int[] GetMassMove(int idPlayer)
	{
		return new[] {15, -15, 17, -17, 10, -10, 6, -6, 0};
	}
	
	public override int GetMaxCountSquare()
	{
		return 1;
	}
}
