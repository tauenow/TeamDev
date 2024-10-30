using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSelectAnim : MonoBehaviour
{
	[Header("共有オブジェクト")]
	[SerializeField] private StageScriptableObject scriptableObject;
	[Header("触れなくするボタン")]
	[SerializeField] private List<Button> buttonList;
	[Header("アニメーションする画像")]
	[SerializeField] private Image image;
	[Header("消すキャンバス")]
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
