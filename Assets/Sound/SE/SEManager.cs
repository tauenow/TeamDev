using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private bool isPlaying; // �Đ������ǂ����������t���O

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAudioMixerGroup(AudioMixerGroup mixerGroup)
    {
        if (audioSource != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void AddAudioClip(string clipName, AudioClip clip)
    {
        if (!audioClips.ContainsKey(clipName))
        {
            audioClips.Add(clipName, clip);
        }
    }

    public void PlaySE(string clipName)
    {
        if (audioClips.TryGetValue(clipName, out AudioClip clip))
        {
            //audioSource.PlayOneShot(clip); // ��ɍĐ�

            // �t���O��ݒ肵�A�R���[�`�����J�n
            if (clipName == "ColorChange")
            {
                Debug.Log(isPlaying);
                if (!isPlaying) // �Đ����łȂ��ꍇ�̂݃t���O��ݒ�
                {
                    Debug.Log("�Đ����܂�");
                    isPlaying = true; // �Đ����t���O���Z�b�g
                    audioSource.PlayOneShot(clip); // ��ɍĐ�

                    StartCoroutine(ResetPlayingFlag(clip.length)); // �R���[�`�����J�n
                }
            }
            else
            {
                audioSource.PlayOneShot(clip); // ��ɍĐ�
            }
        }
        else
        {
            Debug.LogWarning($"SEManager: Clip '{clipName}' not found!");
        }
    }

    private IEnumerator ResetPlayingFlag(float duration)
    {
        yield return new WaitForSeconds(duration); // �N���b�v�̒��������ҋ@
        isPlaying = false; // �Đ����t���O�����Z�b�g
    }
}
