using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResizer : MonoBehaviour
{
    [SerializeField] private Slider positionSlider;  // �X���C�_�[�̎Q��
    [SerializeField] private Image targetImage;      // �Ǐ]����Image�̎Q��

    [SerializeField] private Vector2 moveRange;      // Image�̓����͈́iX���j
    [SerializeField] private Vector2 scaleRange;     // Image�̃X�P�[���͈�

    private void Update()
    {
        // Image�̈ʒu�ƃX�P�[���X�V
        UpdateImagePositionAndScale();
    }

    private void UpdateImagePositionAndScale()
    {
        // �X���C�_�[�̒l�𐳋K�����Ď擾
        float normalizedValue = GetNormalizedSliderValue();

        // �ʒu�ƃX�P�[���ݒ�
        SetImagePosition(normalizedValue);
        SetImageScale(normalizedValue);
    }

    private float GetNormalizedSliderValue()
    {
        // �X���C�_�[�̌��݂̒l���擾���A�ŏ��l�ƍő�l�Ɋ�Â��Đ��K��
        float sliderValue = positionSlider.value;
        return (sliderValue - positionSlider.minValue) / (positionSlider.maxValue - positionSlider.minValue);
    }

    private void SetImagePosition(float normalizedValue)
    {
        // �����W���X�V
        float targetXPosition = Mathf.Lerp(moveRange.x, moveRange.y, normalizedValue);

        // �ʒu��ݒ�
        targetImage.rectTransform.anchoredPosition = new Vector2(targetXPosition, targetImage.rectTransform.anchoredPosition.y);
    }

    private void SetImageScale(float normalizedValue)
    {
        // �X�P�[�����v�Z
        float scale = Mathf.Lerp(scaleRange.x, scaleRange.y, normalizedValue);

        // �X�P�[���ݒ�
        targetImage.rectTransform.localScale = new Vector3(scale, scale, 1f); // Z���͌Œ�
    }
}
