using System.Collections;
using UnityEngine;

public class PlayerAnimetion : MonoBehaviour
{
    private Animator animator;
    private float currentTime;
    private bool yubi = false;

    void Start()
    {
        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // MapManager.floorChangeがtrueになった場合、「Yubisasi」アニメーションを再生
        if (!yubi && MapManager.floorChange)
        {
            yubi = true;
            PlayAnimation("Yubisasi");
        }

        // PlayerControlスクリプトのGetClearがtrueの場合、「Jump」アニメーションを再生
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerControl>().GetClear() == true)
        {
            Debug.Log("Jumpアニメーションを再生");
            PlayAnimation("Jump");
        }

        // PlayerControlスクリプトのGetOnPlayerMoveがtrueの場合、「Run」アニメーションを再生
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerControl>().GetOnPlayerMove() == true)
        {
            Debug.Log("Runアニメーションを再生");
            PlayAnimation("Run");
        }
    }

    // 指定されたアニメーション名でアニメーションを再生し、再生完了後にIdleに戻す
    private void PlayAnimation(string animationName)
    {
        animator.CrossFade(animationName, 0.2f);
        StartCoroutine(ResetIdle(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    // アニメーションが終わったらIdleに戻すコルーチン
    private IEnumerator ResetIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Idle();
    }

    // Idleアニメーションを再生し、状態をリセット
    public void Idle()
    {
        animator.CrossFade("Idle", 0.2f);
        yubi = false; // 状態リセット
    }
}
