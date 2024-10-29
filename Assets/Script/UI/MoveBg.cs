using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoveBg : MonoBehaviour
{
	[SerializeField] private StageScriptableObject scriptableNum;
	[SerializeField] private RectTransform imageTransform;
	[SerializeField] private Canvas cloudCanvas;
	private static int DifficultNum;
	[SerializeField] private Button[] buttons;
	[SerializeField] private InputFlick flick;
	private bool isMove = false;

	// Start is called before the first frame update
	void Start()
	{
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
		if (flick.GetNowFlick() == InputFlick.FlickDirection.LEFT ||
			flick.GetNowFlick() == InputFlick.FlickDirection.RIGHT ||
			flick.GetNowSwipe() == InputFlick.SwipeDirection.LEFT ||
			flick.GetNowSwipe() == InputFlick.SwipeDirection.RIGHT)
		{
			BackGroundMove();
		}
	}

	public void BackGroundMove()
	{
		switch (scriptableNum.DifficultyIndex)
		{
			case 1:
				imageTransform.transform.DOLocalMoveX(2160.0f, 1.0f, true).SetEase(Ease.InOutCirc)
					.OnStart(() =>
					{
						isMove = true;

						cloudCanvas.enabled = false;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = false;
						}

					})
					.OnComplete(() =>
					{
						cloudCanvas.enabled = true;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = true;
						}

						isMove = false;
					});
				break;
			case 2:
				imageTransform.transform.DOLocalMoveX(0.0f, 1.0f, true).SetEase(Ease.InOutCirc)
					.OnStart(() =>
					{
						isMove = true;

						cloudCanvas.enabled = false;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = false;
						}

					})
					.OnComplete(() =>
					{
						cloudCanvas.enabled = true;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = true;
						}

						isMove = false;
					});
				break;
			case 3:
				imageTransform.transform.DOLocalMoveX(-2160.0f, 1.0f, true).SetEase(Ease.InOutCirc)
					.OnStart(() =>
					{
						isMove = true;

						cloudCanvas.enabled = false;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = false;
						}

					})
					.OnComplete(() =>
					{
						cloudCanvas.enabled = true;

						for (int i = 0; i < buttons.Length; i++)
						{
							buttons[i].enabled = true;
						}

						isMove = false;
					});
				break;
		}
	}
}
