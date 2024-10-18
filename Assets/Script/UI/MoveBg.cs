using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBg : MonoBehaviour
{
	[SerializeField] private StageScriptableObject scriptableNum;
	[SerializeField] private RectTransform imageTransform;
	private static int DifficultNum;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(scriptableNum.DifficultyIndex);

		switch (scriptableNum.DifficultyIndex)
		{
			case 1:
				imageTransform.transform.SetLocalPositionAndRotation(new Vector3(2160.0f, 0.0f, 0.0f), Quaternion.identity);
				break;
			case 2:
				imageTransform.transform.SetLocalPositionAndRotation(new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
				break;
			case 3:
				imageTransform.transform.SetLocalPositionAndRotation(new Vector3(-2160.0f, 0.0f, 0.0f), Quaternion.identity);
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void BackGroundMove()
	{
		switch (scriptableNum.DifficultyIndex)
		{
			case 1:

				imageTransform.transform.DOLocalMoveX(2160.0f, 1.0f, true).SetEase(Ease.InOutCirc);
				break;
			case 2:
				imageTransform.transform.DOLocalMoveX(0.0f, 1.0f, true).SetEase(Ease.InOutCirc);
				break;
			case 3:
				imageTransform.transform.DOLocalMoveX(-2160.0f, 1.0f, true).SetEase(Ease.InOutCirc);
				break;
		}
	}
}
