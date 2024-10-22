using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClearAnimManager : MonoBehaviour
{
	// �X�e�[�W�}�l�[�W���[
	private StageSelectManager ssMng;
	// �ړ�������{�^��
	[SerializeField] private Button[] moveButton;
	// �ړ�������摜
	[SerializeField] private Image[] moveImage;

	// Start is called before the first frame update
	void Start()
	{
		ssMng = GetComponent<StageSelectManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (ssMng.isClear)
		{
			// �摜�̈ړ�
			moveImage[0].transform.DOMoveY(1150.0f, 1.0f).SetEase(Ease.OutCubic);
			moveImage[1].transform.DOMoveY(1150.0f, 1.0f).SetEase(Ease.OutCubic);
			// �{�^���̈ړ�
			moveButton[0].transform.DOMoveY(-1150.0f, 1.0f).SetEase(Ease.OutCubic);
			moveButton[1].transform.DOMoveY(-1150.0f, 1.0f).SetEase(Ease.OutCubic);
		}
	}
}
