using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class WidgetAlphaAnim : MonoBehaviour
{
	[Header("�_�Ŏ��Ԃ̊Ԋu")]
	[SerializeField]
	private float blinkIntervalTime = 1.5f;

	[Header("�C�[�W���O�̐ݒ�")]
	[SerializeField]
	private Ease easeType = Ease.Linear;

	// Start is called before the first frame update
	void Start()
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.DOFade(0.0f, blinkIntervalTime).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
