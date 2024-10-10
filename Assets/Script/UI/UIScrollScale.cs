using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollScale : MonoBehaviour
{
    public ScrollRect scrollRect;        // ScrollRectコンポーネント
    public Slider slider;                 // スライダー
    public RectTransform targetUI;        // サイズを変更するUI要素
    public float minScale = 0.5f;         // 最小スケール
    public float maxScale = 1.5f;         // 最大スケール

    private void Start()
    {
        // スライダーの最大値を設定
        slider.maxValue = 1.0f;
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // 初期のスクロール位置を設定
        scrollRect.horizontalNormalizedPosition = slider.value;
        // 初期のUIサイズを設定
        UpdateUIScale(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        // スライダーの値に基づいてScrollRectのスクロール位置を変更
        scrollRect.horizontalNormalizedPosition = value;
        // UI要素のサイズを更新
        UpdateUIScale(value);
    }

    private void UpdateUIScale(float value)
    {
        // スライダーの値に基づいてスケールを計算
        float scaleValue = Mathf.Lerp(minScale, maxScale, value);
        targetUI.localScale = new Vector3(scaleValue, scaleValue, 1);
    }
}
