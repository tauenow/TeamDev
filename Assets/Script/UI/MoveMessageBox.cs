using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoveMessageBox : MonoBehaviour
{
	[SerializeField] private StageScriptableObject scriptableObject;
	[SerializeField] private Image image;
	[SerializeField] private GameObject[] prefab;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Vector3[] generatePos;

	private int createCount = 0;
	private bool isMove = false;

	// Update is called once per frame
	void Update()
	{
		Debug.Log(scriptableObject.textIndex);

		switch (scriptableObject.textIndex)
		{
			case 0:
				if (isMove == false)
				{
					moveMassageBox();
				}
				break;
			case 1:
				isMove = false;
				break;
			case 4:
				if (createCount < 1)
				{
					CreateImage(0);
				}

				break;
			case 5:
				if (isMove == false)
				{
					moveMassageBox();
				}

				if (GameObject.Find("Finger1(Clone)") != null)
				{
					createCount--;
					Destroy(GameObject.Find("Finger1(Clone)"));
				}

				if (createCount < 1)
				{
					CreateImage(1);
				}
				break;
			case 6:
				if (GameObject.Find("Finger2(Clone)") != null)
				{
					createCount--;
					Destroy(GameObject.Find("Finger2(Clone)"));
				}

				isMove = false;
				break;
			case 7:
				if (isMove == false)
				{
					moveMassageBox();
				}
				break;
			default:
				break;
		}
	}

	void moveMassageBox()
	{
		isMove = true;
		switch (scriptableObject.textIndex)
		{
			case 0:
				image.transform.DOLocalMoveY(-560.0f, 1.0f, false).SetEase(Ease.OutBack);
				break;
			case 5:
				image.transform.DOLocalMoveY(520.0f, 1.0f, false).SetEase(Ease.OutCubic);
				break;
			case 7:
				image.transform.DOLocalMoveY(-1300.0f, 1.0f, false).SetEase(Ease.InBack).
					OnComplete(() =>
					{
						Destroy(image);
						Destroy(GameObject.Find("TutorialText"));
					});
				break;
			default:
				break;
		}
	}

	void CreateImage(int CreateNum)
	{
		Vector3 CreatePos1 = generatePos[CreateNum];
		GameObject createPrefab = default;
		switch (CreateNum)
		{
			case 0:
				createPrefab = Instantiate(prefab[CreateNum], CreatePos1, Quaternion.identity);
				break;
			case 1:
				createPrefab = Instantiate(prefab[CreateNum], CreatePos1, new Quaternion(0.0f, 0.0f, 180.0f, 0.0f));
				break;
		}

		createPrefab.transform.SetParent(canvas.transform, false);
		createCount++;
	}
}
