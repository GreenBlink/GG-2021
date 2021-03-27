using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VictoryRule", menuName = "Victory Rule", order = 51)]
public class VictoryRule : Rule
{
	public enum TypeVictoryRule
	{
		KillKing,
		KillAll,
		KillList
	}
	
	public TypeVictoryRule m_TypeVictoryRule;
	public Piece.TypePiece m_TypePiece;

	[HideInInspector] public RulesManager.TypeRule m_TypeRule = RulesManager.TypeRule.Victory;
}
