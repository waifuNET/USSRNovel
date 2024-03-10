using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class UNVDecoder : MonoBehaviour
{
	public List<string> ReadUNVFile(string FilePath)
	{
		string path = Path.Combine(Application.streamingAssetsPath, FilePath);
		
		List<string> strings = new List<string>();

		using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
		{
			string line = String.Empty;
			while((line = sr.ReadLine()) != null)
			{
				line = line.Trim();
				if (line != "")
				{
					strings.Add(line);
				}
			}
		}

		return strings;
	}
	public List<DialogElement> Decode(string FilePath)
	{
		List<DialogElement> dialogElements = new List<DialogElement>();

		List<string> strings = new List<string>();
		strings = ReadUNVFile(FilePath);

		List<List<string>> stringsCharps = GetElementsSplited(strings);

		for (int i = 0; i < stringsCharps.Count; i++)
		{
			dialogElements.Add(GetElement(stringsCharps[i], dialogElements.Count - 1 > 0 ? dialogElements[dialogElements.Count - 1] : null));
		}

		return dialogElements;
	}

	private List<List<string>> GetElementsSplited(List<string> strings)
	{
		List<List<string>> stringsCharps = new List<List<string>>();

		for (int i = 0; i < strings.Count; i++)
		{
			if (strings[i][0] == '#')
			{
				stringsCharps.Add(new List<string>());
				stringsCharps[stringsCharps.Count - 1].Add(strings[i]);
			}
			else
			{
				stringsCharps[stringsCharps.Count - 1].Add(strings[i]);
			}
		}

		return stringsCharps;
	}

	private DialogElement GetElement(List<string> local, DialogElement before)
	{
		DialogElement element = new DialogElement();

		for (int j = 0; j < local.Count; j++)
		{
			if (local[j][0] == '#')
			{
				if (local[j].Split('#')[1].Contains(':'))
				{
					element.SetName(local[j].Split('#')[1].Split(':')[0].Trim());
					element.SetMessage(local[j].Split('#')[1].Split(':')[1].Trim());
				}
			}
			else
			{
				element.AddNewAction(local[j].Trim());
			}
			if (before != null)
				element.SetOldActions(before.GetNewActions());
		}

		return element;
	}
}
