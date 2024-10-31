using UnityEngine;
using UnityEngine.Audio;

public class AudioLoader : MonoBehaviour
{
    [SerializeField]
    private AudioClip selectClip; // "Select"�̉����N���b�v
    [SerializeField]
    private AudioClip colorChangeClip;
    [SerializeField]
    private AudioClip BlockFitClip;
    [SerializeField]
    private AudioClip BlockCompletionClip;
    [SerializeField]
    private AudioClip CongratulationClip;
    [SerializeField]
    private AudioClip PlayerwalkClip;
    [SerializeField]
    private AudioClip RetryClip;
    [SerializeField]
    private AudioMixerGroup audioMixerGroup; // �o�͐��AudioMixerGroup


    private void Start()
    {
        SEManager.Instance.SetAudioMixerGroup(audioMixerGroup); // AudioSource�̏o�͐���w��

        // SEManager�ɉ����N���b�v��ǉ�
        SEManager.Instance.AddAudioClip("Select", selectClip);
        SEManager.Instance.AddAudioClip("ColorChange", colorChangeClip);
        SEManager.Instance.AddAudioClip("Block_Fit", BlockFitClip);
        SEManager.Instance.AddAudioClip("Block_Completion", BlockCompletionClip);
        SEManager.Instance.AddAudioClip("Congratulation", CongratulationClip);
        SEManager.Instance.AddAudioClip("Player_walk", PlayerwalkClip);
        SEManager.Instance.AddAudioClip("Retry", RetryClip);
    }
}
