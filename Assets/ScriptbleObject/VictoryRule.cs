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
		KillPawn
	}
	
	public TypeVictoryRule m_TypeVictoryRule;

	[HideInInspector] public RulesManager.TypeRule m_TypeRule = RulesManager.TypeRule.Victory;
}
