using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSelectAnim : MonoBehaviour
{
	[Header("���L�I�u�W�F�N�g")]
	[SerializeField] private StageScriptableObject scriptableObject;
	[Header("�G��Ȃ�����{�^��")]
	[SerializeField] private List<Button> buttonList;
	[Header("�A�j���[�V��������摜")]
	[SerializeField] private Image image;
	[Header("�����L�����o�X")]
	[SerializeField] private Canvas canvas;

	private bool isAnim = false;


	// Update is called once per frame
	void Update()
	{
		if (scriptableObject.oldIsClear[scriptableObject.StageNum] !=
			scriptableObject.isClearList[scriptableObject.StageNum])
		{
			if (isAnim = true)
			{
				return;
			}

			ClearAnim();
		}
	}

	void ClearAnim()
	{
		isAnim = true;


	}
}
