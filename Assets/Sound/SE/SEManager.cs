using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    // �V���O���g���C���X�^���X (���̃X�N���v�g����A�N�Z�X���邽�߂̋��L�C���X�^���X)
    public static SEManager Instance;

    // SE�i�T�E���h�G�t�F�N�g�j���Đ����邽�߂�AudioSource
    private AudioSource audioSource;

    // Awake���\�b�h�̓Q�[���I�u�W�F�N�g���V�[���Ƀ��[�h���ꂽ�Ƃ��ɍŏ��ɌĂ΂��
    private void Awake()
    {
        // �V���O���g���p�^�[���̐ݒ�: �C���X�^���X���܂��Ȃ��ꍇ�́A���̃Q�[���I�u�W�F�N�g���C���X�^���X�Ƃ��Đݒ�
        if (Instance == null)
        {
            Instance = this;
            // �V�[�����؂�ւ���Ă����̃Q�[���I�u�W�F�N�g��j�����Ȃ�
            DontDestroyOnLoad(gameObject);
            // AudioSource�R���|�[�l���g���擾
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // ���łɃC���X�^���X�����݂���ꍇ�͐V�����Q�[���I�u�W�F�N�g���폜
            Destroy(gameObject);
        }
    }

    // �T�E���h�G�t�F�N�g���Đ����邽�߂̃��\�b�h
    public void PlaySE(AudioClip seClip)
    {
        // AudioClip��null�łȂ��ꍇ�ɍĐ����s��
        if (seClip != null)
        {
            // �w�肳�ꂽAudioClip���Đ� (PlayOneShot��SE���d�˂čĐ�����̂ɓK���Ă���)
            audioSource.PlayOneShot(seClip);
        }
        else
        {
            // null�̏ꍇ�͌x�����b�Z�[�W��\��
            Debug.LogWarning("PlaySE: SE�N���b�v��null�ł��BAudioClip���ݒ肳��Ă��邩�m�F���Ă��������B");
        }
    }
}
