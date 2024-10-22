using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLockEnable : MonoBehaviour
{
	// ScriptableObject
	[SerializeField] private StageScriptableObject scriptableObj;
	// �ς���{�^��
	[Header("�ς���{�^��")]
	[SerializeField] private Button[] changeButtons;
	// �ς���L�����o�X
	[Header("�ς���L�����o�X")]
	[SerializeField] private Canvas[] changeCanvasArray;

	private void Start()
	{
		SetLock();
	}

	public void SetLock()
	{
		switch (scriptableObj.DifficultyIndex)
		{
			case 1:
				changeCanvasArray[0].enabled = false;

				for (int i = 1; i < 3; i++)
				{
					if (scriptableObj.isClearList[i - 1] == true)
					{
						changeCanvasArray[i].enabled = false;
						changeButtons[i].enabled = true;
					}
					else
					{
						changeCanvasArray[i].enabled = true;
						changeButtons[i].enabled = false;
					}
				}
				break;
			case 2:
				for (int i = 0; i < 3; i++)
				{
					if (scriptableObj.isClearList[i + 2] == true)
					{
						changeCanvasArray[i].enabled = false;
						changeButtons[i].enabled = true;
					}
					else
					{
						changeCanvasArray[i].enabled = true;
						changeButtons[i].enabled = false;
					}
				}
				break;
			case 3:
				for (int i = 0; i < 3; i++)
				{
					if (scriptableObj.isClearList[i + 5] == true)
					{
						changeCanvasArray[i].enabled = false;
						changeButtons[i].enabled = true;
					}
					else
					{
						changeCanvasArray[i].enabled = true;
						changeButtons[i].enabled = false;
					}
				}
				break;
		}
	}
}
