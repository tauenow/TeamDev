using System.Collections;
using UnityEngine;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator animator;
    private float currentTime;
    private bool yubi = false;

    void Start()
    {
        // Animator�R���|�[�l���g���擾
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // MapManager.floorChange��true�ɂȂ����ꍇ�A�uYubisasi�v�A�j���[�V�������Đ�
        if (!yubi && MapManager.floorChange)
        {
            yubi = true;
            PlayAnimation("Yubisasi");
        }

        // PlayerControl�X�N���v�g��GetClear��true�̏ꍇ�A�uJump�v�A�j���[�V�������Đ�
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerControl>().GetClear() == true)
        {
            Debug.Log("Jump�A�j���[�V�������Đ�");
            PlayAnimation("Jump");
        }

        // PlayerControl�X�N���v�g��GetOnPlayerMove��true�̏ꍇ�A�uRun�v�A�j���[�V�������Đ�
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerControl>().GetOnPlayerMove() == true)
        {
            Debug.Log("Run�A�j���[�V�������Đ�");
            PlayAnimation("Run");
        }
    }

    // �w�肳�ꂽ�A�j���[�V�������ŃA�j���[�V�������Đ����A�Đ��������Idle�ɖ߂�
    private void PlayAnimation(string animationName)
    {
        animator.CrossFade(animationName, 0.2f);
        StartCoroutine(ResetIdle(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    // �A�j���[�V�������I�������Idle�ɖ߂��R���[�`��
    private IEnumerator ResetIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Idle();
    }

    // Idle�A�j���[�V�������Đ����A��Ԃ����Z�b�g
    public void Idle()
    {
        animator.CrossFade("Idle", 0.2f);
        yubi = false; // ��ԃ��Z�b�g
    }
}
