using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollScale : MonoBehaviour
{
    public ScrollRect scrollRect;        // ScrollRect�R���|�[�l���g
    public Slider slider;                 // �X���C�_�[
    public RectTransform targetUI;        // �T�C�Y��ύX����UI�v�f
    public float minScale = 0.5f;         // �ŏ��X�P�[��
    public float maxScale = 1.5f;         // �ő�X�P�[��

    private void Start()
    {
        // �X���C�_�[�̍ő�l��ݒ�
        slider.maxValue = 1.0f;
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // �����̃X�N���[���ʒu��ݒ�
        scrollRect.horizontalNormalizedPosition = slider.value;
        // ������UI�T�C�Y��ݒ�
        UpdateUIScale(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        // �X���C�_�[�̒l�Ɋ�Â���ScrollRect�̃X�N���[���ʒu��ύX
        scrollRect.horizontalNormalizedPosition = value;
        // UI�v�f�̃T�C�Y���X�V
        UpdateUIScale(value);
    }

    private void UpdateUIScale(float value)
    {
        // �X���C�_�[�̒l�Ɋ�Â��ăX�P�[�����v�Z
        float scaleValue = Mathf.Lerp(minScale, maxScale, value);
        targetUI.localScale = new Vector3(scaleValue, scaleValue, 1);
    }
}
