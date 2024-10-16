using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeINOUT : MonoBehaviour
{
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

    // ���̃V�[�������C���X�y�N�^�[�Ŏw��
    [SerializeField]
    private string nextSceneName;

    // �t�F�[�h�A�E�g���J�n���邽�߂̃t���O
    private bool startFadeOut = false;

    // �Q�[���J�n���Ƀt�F�[�h�C�����J�n
    void Start()
    {
        StartCoroutine(BeginTransitionIn());
    }

    // �t���[�����Ƃ̏����ŁA��ʃ^�b�v�Ńt�F�[�h�A�E�g���J�n
    void Update()
    {
        // �t�F�[�h�A�E�g�\��Ԃ���ʂ��^�b�v���ꂽ�ꍇ�Ƀt�F�[�h�A�E�g���J�n
        if (startFadeOut && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(BeginTransitionOut());
        }
    }

    // �t�F�[�h�C�����J�n����R���[�`��
    IEnumerator BeginTransitionIn()
    {
        // �w�肳�ꂽ���ԂŃt�F�[�h�C���A�j���[�V���������s
        yield return Animate(_transitionIn, 1);

        // �t�F�[�h�C�����Ɏw�肳�ꂽ�C�x���g�����s
        if (OnTransition != null)
        {
            OnTransition.Invoke();
        }

        // �t�F�[�h�C�����I����Ɏ��̃t���[����҂�
        yield return new WaitForEndOfFrame();

        // �t�F�[�h�C��������A�t�F�[�h�A�E�g���\�ɂ���
        startFadeOut = true;
    }

    // �t�F�[�h�A�E�g���J�n����R���[�`��
    IEnumerator BeginTransitionOut()
    {
        // �w�肳�ꂽ���ԂŃt�F�[�h�A�E�g�A�j���[�V���������s
        yield return Animate(_transitionOut, 1);

        // �t�F�[�h�A�E�g���I����Ɏw�肳�ꂽ�C�x���g�����s
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }

        // �t�F�[�h�A�E�g������Ɏ��̃t���[����҂�
        yield return new WaitForEndOfFrame();

        // �t�F�[�h�A�E�g������A���̃V�[���ɐ؂�ւ�
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // �w�肳�ꂽ�}�e���A���ŃA�j���[�V���������s����R���[�`��
    IEnumerator Animate(Material material, float time)
    {
        // UI Image�R���|�[�l���g�Ƀ}�e���A����K�p
        GetComponent<Image>().material = material;

        float current = 0;

        // time�b�����ăt�F�[�h��i�s
        while (current < time)
        {
            // �V�F�[�_�[�� "_Alpha" �p�����[�^�����Ԍo�߂Ɋ�Â��ĕύX
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();

            // �o�ߎ��Ԃ��X�V
            current += Time.deltaTime;
        }

        // �ŏI�I�Ƀt�F�[�h�����S�ɓ����܂��͕s�����ɐݒ�
        material.SetFloat("_Alpha", 1);
    }
}
