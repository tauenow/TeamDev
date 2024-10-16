using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeINOUT : MonoBehaviour
{
    // フェードインに使用するマテリアル
    [SerializeField]
    private Material _transitionIn;

    // フェードアウトに使用するマテリアル
    [SerializeField]
    private Material _transitionOut;

    // フェード中に呼び出されるイベント
    [SerializeField]
    private UnityEvent OnTransition;

    // フェードが完了したときに呼び出されるイベント
    [SerializeField]
    private UnityEvent OnComplete;

    // 次のシーン名をインスペクターで指定
    [SerializeField]
    private string nextSceneName;

    // フェードアウトを開始するためのフラグ
    private bool startFadeOut = false;

    // ゲーム開始時にフェードインを開始
    void Start()
    {
        StartCoroutine(BeginTransitionIn());
    }

    // フレームごとの処理で、画面タップでフェードアウトを開始
    void Update()
    {
        // フェードアウト可能状態かつ画面がタップされた場合にフェードアウトを開始
        if (startFadeOut && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(BeginTransitionOut());
        }
    }

    // フェードインを開始するコルーチン
    IEnumerator BeginTransitionIn()
    {
        // 指定された時間でフェードインアニメーションを実行
        yield return Animate(_transitionIn, 1);

        // フェードイン中に指定されたイベントを実行
        if (OnTransition != null)
        {
            OnTransition.Invoke();
        }

        // フェードインが終了後に次のフレームを待つ
        yield return new WaitForEndOfFrame();

        // フェードイン完了後、フェードアウトを可能にする
        startFadeOut = true;
    }

    // フェードアウトを開始するコルーチン
    IEnumerator BeginTransitionOut()
    {
        // 指定された時間でフェードアウトアニメーションを実行
        yield return Animate(_transitionOut, 1);

        // フェードアウトが終了後に指定されたイベントを実行
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }

        // フェードアウト完了後に次のフレームを待つ
        yield return new WaitForEndOfFrame();

        // フェードアウト完了後、次のシーンに切り替え
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    // 指定されたマテリアルでアニメーションを実行するコルーチン
    IEnumerator Animate(Material material, float time)
    {
        // UI Imageコンポーネントにマテリアルを適用
        GetComponent<Image>().material = material;

        float current = 0;

        // time秒かけてフェードを進行
        while (current < time)
        {
            // シェーダーの "_Alpha" パラメータを時間経過に基づいて変更
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();

            // 経過時間を更新
            current += Time.deltaTime;
        }

        // 最終的にフェードを完全に透明または不透明に設定
        material.SetFloat("_Alpha", 1);
    }
}
