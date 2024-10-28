using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    // シングルトンインスタンス (他のスクリプトからアクセスするための共有インスタンス)
    public static SEManager Instance;

    // SE（サウンドエフェクト）を再生するためのAudioSource
    private AudioSource audioSource;

    // Awakeメソッドはゲームオブジェクトがシーンにロードされたときに最初に呼ばれる
    private void Awake()
    {
        // シングルトンパターンの設定: インスタンスがまだない場合は、このゲームオブジェクトをインスタンスとして設定
        if (Instance == null)
        {
            Instance = this;
            // シーンが切り替わってもこのゲームオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
            // AudioSourceコンポーネントを取得
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // すでにインスタンスが存在する場合は新しいゲームオブジェクトを削除
            Destroy(gameObject);
        }
    }

    // サウンドエフェクトを再生するためのメソッド
    public void PlaySE(AudioClip seClip)
    {
        // AudioClipがnullでない場合に再生を行う
        if (seClip != null)
        {
            // 指定されたAudioClipを再生 (PlayOneShotはSEを重ねて再生するのに適している)
            audioSource.PlayOneShot(seClip);
        }
        else
        {
            // nullの場合は警告メッセージを表示
            Debug.LogWarning("PlaySE: SEクリップがnullです。AudioClipが設定されているか確認してください。");
        }
    }
}
