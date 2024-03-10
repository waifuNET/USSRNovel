using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogElement
{
	[SerializeField] private string name = null;
	[SerializeField] private string message = null;

	[SerializeField] private List<string> oldActions = new List<string>();
	[SerializeField] private List<string> newActions = new List<string>();

	public DialogElement(string name, string message, List<string> newActions, List<string> oldActions)
	{
		this.name = name;
		this.message = message;
		this.newActions = newActions;
		this.oldActions = oldActions;
	}

	public DialogElement() { }

	public string GetName() { return name; }
	public string GetMessage() { return message; }
	public List<string> GetOldActions() {  return oldActions; }
	public List<string> GetNewActions() {  return newActions; }
	public void SetName(string name) { this.name = name;}
	public void SetMessage(string message) {  this.message = message;}
	public void SetOldActions(List<string> strings) { this.oldActions = strings;}
	public void SetNewActions(List<string> strings) { this.newActions = strings;}
	public void AddNewAction(string line) { this.newActions.Add(line);}
}

public class StoryLine : MonoBehaviour
{
	[SerializeField] private int dialogID = -1;
	[SerializeField] private List<DialogElement> elements = new List<DialogElement>();

	public UNVDecoder decoder;

	public TextMeshProUGUI Name;
	public TextMeshProUGUI Message;

	public ActionsHandle actionsHandle;

	private void Start()
	{
		elements = decoder.Decode("Dialogs/Dev/Dev.uvn");
		NextLine();
	}

	private void NextLine()
	{
		dialogID += 1;

		if(dialogID > elements.Count - 1)
			dialogID -= 1;
		else if(dialogID < 0) dialogID = 0;

		Name.text = elements[dialogID].GetName();
		Message.text = elements[dialogID].GetMessage();
		actionsHandle.RunActions(elements[dialogID].GetNewActions());

		if (elements[dialogID].GetMessage() == null && elements[dialogID].GetMessage() == null) { NextLine(); };
	}

	#region GUI iter
	public void GUI_NextLine()
	{
		NextLine();
	}
	#endregion
}
