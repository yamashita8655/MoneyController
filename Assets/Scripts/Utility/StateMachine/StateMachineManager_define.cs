/*
 * @file StateMachineManager_define.cs
 * ステートマシンの種類を記載する定義クラス.
 * @author 山下
 */

using UnityEngine;
using System.Collections;

/// <summary>
///	ステートマシンの種類を記載する定義クラス.
/// </summary>
public enum StateMachineName : int
{
	Test,
	InGame,
	Max,
};

/// <summary>
/// </summary>
public enum InGame1State : int
{
	Initialize = 0,
	StartEffect,
	UserWait,
	ClearCheck,
	MoveEffect,
	ClearEffect,
	FailureEffect,
	End
};

/// <summary>
/// </summary>
public enum InGame3State : int
{
	Initialize = 0,
	StartEffect,
	SpawnFirstButton,
	UserWait,
	CheckPower,
	SpawnSecondButton,
	ClearEffect,
	FailureEffect,
	End
};

