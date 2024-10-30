using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClearSelectAnim : MonoBehaviour
{
	[Header("���L�I�u�W�F�N�g")]
	[SerializeField] private StageScriptableObject scriptableObject;
	[Header("�G��Ȃ�����{�^��")]
	[SerializeField] private List<Button> buttonList;
	[Header("�A�j���[�V��������摜")]
	[SerializeField] private List<Image> image;
	[Header("�����L�����o�X")]
	[SerializeField] private List<CanvasGroup> canvas;

	private void Start()
	{
		if (scriptableObject.oldIsClear[scriptableObject.StageNum] !=
			scriptableObject.isClearList[scriptableObject.StageNum])
		{
			if (scriptableObject.StageNum == 0 || scriptableObject.StageNum == 3 || scriptableObject.StageNum == 6)
				ClearAnim(0);
			else if (scriptableObject.StageNum == 1 || scriptableObject.StageNum == 4 || scriptableObject.StageNum == 7)
				ClearAnim(1);
			else if (scriptableObject.StageNum == 2 || scriptableObject.StageNum == 5)
				ClearAnim(-1);
		}
	}

	void ClearAnim(int buttonNum)
	{
		Debug.Log("�Ă΂�Ă܂�");

		// �{�^����G��Ȃ��l��
		for (int i = 0; i < buttonList.Count; i++)
		{
			buttonList[i].enabled = false;
		}

		// �V�[�N�G���X���m��
		var sequence = DOTween.Sequence();

		// ���O���J����A�j���[�V����
		sequence.Append(image[buttonNum + 1].transform.DOLocalMoveY(20.0f, 1.0f));
		// ���߂̃A�j���[�V����
		sequence.Append(canvas[buttonNum + 1].DOFade(0.0f, 1.0f));
		sequence.Play().OnComplete(() =>
		{
			// ������{�^����G���l��
			for (int i = 0; i < buttonList.Count; i++)
			{
				buttonList[i].enabled = true;
			}
		});
	}
}
