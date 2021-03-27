using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ForwardAndBackMove : StandartMove
{
	public override int[] GetMassMove(int idPlayer)
	{
		return new[] {8, -8, 0};
	}
	
	public override int GetMaxCountSquare()
	{
		return 1;
	}
}
