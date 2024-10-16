using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    // UI�}�l�[�W�����擾
    private UIManager UImng;

    // �V�F�[�_�[�̃}�e���A�����Q��
    public Material material;

    // �⊮����l�̊J�n�_�ƏI���_
    public float startValue = 1.0f; // �s����
    public float endValue = -1.0f;   // ����

    // �t�F�[�h�̎���
    public float fadeDuration = 2.0f;

    // �V�[����
    public string nextSceneName;

    // �t�F�[�h��Ԃ��Ǘ�
    private bool isFading = false;
    private float currentTime = 0.0f;

    // �V�F�[�_�[�̃v���p�e�B��
    private string shaderPropertyName = "_Val";

    void Start()
    {
        UImng = GetComponent<UIManager>();

        // �}�e���A�����ݒ肳��Ă��Ȃ��ꍇ�A�����Ŏ擾
        if (material == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                material = renderer.material;
            }
        }
        // �V�F�[�_�[�̏����l��ݒ�
        material.SetFloat(shaderPropertyName, startValue);
    }

    void Update()
    {
        // �t�F�[�h���ł���΁A�t�F�[�h��i�s
        if (UImng.isFade)
        {
            currentTime += Time.deltaTime;

            // �t�F�[�h�i�s�x���v�Z
            float t = currentTime / fadeDuration;
            float currentVal = Mathf.Lerp(startValue, endValue, t);
            material.SetFloat(shaderPropertyName, currentVal);

            // �t�F�[�h������������V�[����ύX
            if (currentTime >= fadeDuration)
            {
                SceneManager.LoadScene(nextSceneName); // ���̃V�[���ɐ؂�ւ�
                isFading = false;  // �t�F�[�h������Ƀt���O�����Z�b�g
            }
        }
    }

    // �t�F�[�h���J�n����֐� (�{�^���������ꂽ��Ă΂��)
    public void StartFade()
    {
        if (!isFading && UImng.isFade)
        {
            isFading = true;  // �t�F�[�h���J�n
            currentTime = 0.0f;  // �t�F�[�h���Ԃ����Z�b�g
        }
    }
}
