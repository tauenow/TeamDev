using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isYubisasiPlaying = false; // Yubisasi�̏�Ԃ��Ǘ�

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleYubisasiAnimation();
        HandleOtherAnimations();
    }

    // Yubisasi�A�j���[�V�����̊Ǘ�
    private void HandleYubisasiAnimation()
    {
        if (!isYubisasiPlaying && MapManager.floorChange)
        {
            PlayYubisasi();
        }
    }

    // Yubisasi�Đ�
    private void PlayYubisasi()
    {
        animator.SetBool("Yubisasi", true);
        isYubisasiPlaying = true;
        StartCoroutine(OffYubisasi(1.5f)); // 3�b���Yubisasi���I������
    }

    // ���̃A�j���[�V�����̊Ǘ�
    private void HandleOtherAnimations()
    {
        if (isYubisasiPlaying) return; // Yubisasi���Đ����̏ꍇ�A���̃A�j���[�V�����𖳌��ɂ���

        PlayerControl playerControl = GameObject.Find("Player(Clone)").GetComponent<PlayerControl>();

        if (playerControl.GetClear())
        {
            SetAnimationState("Jump");
        }
        else if (playerControl.GetOnPlayerMove())
        {
            SetAnimationState("Run");
        }
        else
        {
            SetAnimationState("Idle");
        }
    }

    // �A�j���[�V������Ԃ̐ݒ�
    private void SetAnimationState(string state)
    {
        animator.SetBool("Idle", state == "Idle");
        animator.SetBool("Run", state == "Run");
        animator.SetBool("Jump", state == "Jump");
    }

private IEnumerator OffYubisasi(float delay)
{
    yield return new WaitForSeconds(delay);
    animator.SetBool("Yubisasi", false); // Yubisasi���I��
    isYubisasiPlaying = false;
}
}