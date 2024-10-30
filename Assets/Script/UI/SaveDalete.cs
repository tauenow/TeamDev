using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveDalete : MonoBehaviour
{
	[SerializeField] private GameObject prefab;
	[SerializeField] private Canvas canvas;

	public void Create()
	{
		GameObject prefab1 = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
		prefab1.transform.SetParent(canvas.transform, false);
		prefab1.transform.DOScale(new Vector3(1.0f, 1.0f, 0.0f), 0.5f).SetEase(Ease.OutBack);
	}

	public void Complete()
	{
		Destroy();
	}

	public void Destroy()
	{
		Destroy();
	}
}
