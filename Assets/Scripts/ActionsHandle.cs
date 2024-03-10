using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsHandle : MonoBehaviour
{
	public BackgroundHandler BackgroundHandler;
	public AudioHandler AudioHandler;
	public CharacterHandler CharacterHandler;

	public void RunActions(List<string> actions)
	{
		for(int i = 0; i < actions.Count; i++)
		{
			ActionHandle(actions[i]);
		}
	}
	private void ActionHandle(string action)
	{
		if (action.Contains("BG")) { BG_AC(action); }
	}

	public void StopActions(List<string> actions, int index)
	{
		for(int i = 0; i < actions.Count; i++)
		{
			StopActionHandle(actions[i], index);
		}
	}
	private void StopActionHandle(string action, int index)
	{

	}

	#region Actions Handlers
	private void BG_AC(string action)
	{
		action = action.Split(':')[1].Trim();

		string[] actions = action.Split(' ');
		if (actions.Length > 0)
		{
			BackgroundHandler.SetNewBackground(actions[0]);
		}
	}
	#endregion
}
