using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
	[SerializeField] private StageScriptableObject scriptableIndex;
	[SerializeField] private Image[] Clears;

	// Start is called before the first frame update
	void Start()
	{
		switch (scriptableIndex.DifficultyIndex)
		{
			case 1:
				for (int i = 0; i < 3; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i].enabled = true;
					}
					else
					{
						Clears[i].enabled = false;
					}
				}

				break;
			case 2:
				for (int i = 3; i < 6; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i - 3].enabled = true;
					}
					else
					{
						Clears[i - 3].enabled = false;
					}
				}

				break;
			case 3:
				for (int i = 6; i < 9; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i - 6].enabled = true;
					}
					else
					{
						Clears[i - 6].enabled = false;
					}
				}

				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		switch (scriptableIndex.DifficultyIndex)
		{
			case 1:
				for (int i = 0; i < 3; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i].enabled = true;
					}
					else
					{
						Clears[i].enabled = false;
					}
				}
				break;
			case 2:
				for (int i = 3; i < 6; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i - 3].enabled = true;
					}
					else
					{
						Clears[i - 3].enabled = false;
					}
				}
				break;
			case 3:
				for (int i = 6; i < 9; i++)
				{
					if (scriptableIndex.isClearList[i] == true)
					{
						Clears[i - 6].enabled = true;
					}
					else
					{
						Clears[i - 6].enabled = false;
					}
				}
				break;
		}
	}
}
