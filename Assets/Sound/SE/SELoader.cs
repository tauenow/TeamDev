using UnityEngine;

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




    private void Start()
    {
        // SEManager�ɉ����N���b�v��ǉ�
        SEManager.Instance.AddAudioClip("Select", selectClip);
        SEManager.Instance.AddAudioClip("ColorChange", colorChangeClip);
        SEManager.Instance.AddAudioClip("Block_Fit", BlockFitClip);
        SEManager.Instance.AddAudioClip("Block_Completion", BlockCompletionClip);
        SEManager.Instance.AddAudioClip("Congratulation", CongratulationClip);
        SEManager.Instance.AddAudioClip("Player_walk", PlayerwalkClip);
    }
}
