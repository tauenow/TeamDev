using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeINOUT : MonoBehaviour
{
    // ScriptableObject
    [SerializeField]
    private StageScriptableObject scriptableObject;

    // �t�F�[�h�C���Ɏg�p����}�e���A��
    [SerializeField]
    private Material _transitionIn;

    // �t�F�[�h�A�E�g�Ɏg�p����}�e���A��
    [SerializeField]
    private Material _transitionOut;

    // �t�F�[�h���ɌĂяo�����C�x���g
    [SerializeField]
    private UnityEvent OnTransition;

    // �t�F�[�h�����������Ƃ��ɌĂяo�����C�x���g
    [SerializeField]
    private UnityEvent OnComplete;

    // �{�^�����X�g�i�C���X�y�N�^�[�Őݒ�j
    [SerializeField]
    private List<Button> transitionButtons;

    [Header("�{�^���ɑΉ�����V�[�����̃��X�g")]
    // �{�^���ɑΉ�����V�[�����̃��X�g
    [SerializeField]
    private List<string> sceneNames;

    // �t�F�[�h�A�E�g���g���K�[������@�̑I����
    public enum FadeTriggerMode { Tap, Button }

    // ���݂̃g���K�[���@���C���X�y�N�^�[�Őݒ�
    [SerializeField]
    private FadeTriggerMode fadeTriggerMode = FadeTriggerMode.Tap;

    // �Q�[���J�n���Ƀt�F�[�h�C�����J�n
    void Start()
    {

        // �t�F�[�h�C���̃R���[�`�����J�n
        StartCoroutine(BeginTransitionIn());

        // �{�^�����ݒ肳��Ă��āA�{�^�����[�h���I������Ă���ꍇ�ɃN���b�N�C�x���g��ǉ�
        if (fadeTriggerMode == FadeTriggerMode.Button)
        {
            for (int i = 0; i < transitionButtons.Count; i++)
            {
                Button button = transitionButtons[i];
                // �{�^�����N���b�N���ꂽ�Ƃ��ɑΉ�����V�[���ɐ؂�ւ��郊�X�i�[��ǉ�
                if (button != null && i < sceneNames.Count) // sceneNames�̐����`�F�b�N
                {
                    int index = i; // ���[�J���ϐ��ŃL���v�`��
                    button.onClick.AddListener(() =>
                    {
                        HandleButtonTransition(index);
                    });
                }
            }
        }
    }

    // �t���[�����Ƃ̏����ŁA��ʃ^�b�v�Ńt�F�[�h�A�E�g���J�n
    void Update()
    {
        // ��ʃ^�b�v���[�h���I������Ă���ꍇ�A�^�b�v�Ńt�F�[�h�A�E�g���J�n
        if (fadeTriggerMode == FadeTriggerMode.Tap && Input.GetMouseButtonDown(0))
        {
            
            HandleTapTransition();
        }
    }

    // �{�^������̃t�F�[�h�A�E�g�������܂Ƃ߂����\�b�h
    private void HandleButtonTransition(int index)
    {
        StartCoroutine(BeginTransitionOut(sceneNames[index])); // �Ή�����V�[���ɐ؂�ւ�
    }

    private void HandleTapTransition()
    {
        // �ŏ��̃V�[�������g���ăt�F�[�h�A�E�g���J�n
        if (sceneNames.Count > 0)
        {
            StartCoroutine(BeginTransitionOut(sceneNames[0])); // ��Ƃ��čŏ��̃V�[�������g�p
        }
    }

    // �t�F�[�h�C�����J�n����R���[�`��
    IEnumerator BeginTransitionIn()
    {
        // �t�F�[�h�C���A�j���[�V�������w�肳�ꂽ���ԂŎ��s
        yield return Animate(_transitionIn, 1);

        // �t�F�[�h�C�����Ɏw�肳�ꂽ�C�x���g�����s
        OnTransition?.Invoke();

        // �t�F�[�h�C�����I����Ɏ��̃t���[����҂�
        yield return new WaitForEndOfFrame();
    }

    // �t�F�[�h�A�E�g���J�n����R���[�`���i�V�[�����������Ƃ��Ď󂯎��j
    IEnumerator BeginTransitionOut(string sceneName)
    {
        // �t�F�[�h�A�E�g�A�j���[�V�������w�肳�ꂽ���ԂŎ��s
        yield return Animate(_transitionOut, 1);

        // �t�F�[�h�A�E�g���I����Ɏw�肳�ꂽ�C�x���g�����s
        OnComplete?.Invoke();

        // �t�F�[�h�A�E�g������Ɏ��̃t���[����҂�
        yield return new WaitForEndOfFrame();

        // �w�肳�ꂽ�V�[���ɐ؂�ւ�
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            // �t�F�[�h�C�����ĊJ
            StartCoroutine(BeginTransitionIn());
        }
    }

    // �w�肳�ꂽ�}�e���A���ŃA�j���[�V���������s����R���[�`��
    IEnumerator Animate(Material material, float time)
    {
        // UI Image�R���|�[�l���g�Ƀ}�e���A����K�p
        GetComponent<Image>().material = material;

        float current = 0;

        // �w�肳�ꂽ���ԂŃt�F�[�h��i�s
        while (current < time)
        {
            // �V�F�[�_�[�� "_Alpha" �p�����[�^�����Ԍo�߂Ɋ�Â��ĕύX
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame(); // ���̃t���[���܂őҋ@

            // �o�ߎ��Ԃ��X�V
            current += Time.deltaTime;
        }

        // �ŏI�I�Ƀt�F�[�h�����S�ɕs�����ɐݒ�
        material.SetFloat("_Alpha", 1);
    }

    public void FadeToChangeScene()
    {
        // �ŏ��̃V�[�������g���ăt�F�[�h�A�E�g���J�n
        if (sceneNames.Count > 0)
        {
            StartCoroutine(BeginTransitionOut(sceneNames[0])); // ��Ƃ��čŏ��̃V�[�������g�p
        }
    }
}
