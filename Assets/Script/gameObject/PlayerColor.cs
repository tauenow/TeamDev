using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 森口後藤作のプレイヤーの色を変更するスクリプト。
/// コーディングとかくそも無い奴なんで、何かあったら殺してください。
/// </summary>
public class PlayerColor : MonoBehaviour
{
    [Header("共有オブジェクト")][SerializeField] private StageScriptableObject scriptableObject;

    [SerializeField] Texture[] playerColor = new Texture[4];

    //子オブジェクトを格納する変数
    public GameObject cup;
    public GameObject sotai;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの子オブジェクトを取得
        cup = GameObject.Find("cup");
        sotai = GameObject.Find("sotai");

        //stageスクリプトオブジェクトから参照。
        switch (scriptableObject.colorName)
        {
            //MaterialのTextureを書き換えてプレイヤーのカラー変更
            //赤なら配列0番目を
            case "red":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[0];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[0];
                break;
            //青なら配列1番目を
            case "blue":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[1];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[1];
                break;
            //黄色なら配列2番目を
            case "yellow":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[2];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[2];
                break;
            //緑なら配列3番目を
            case "green":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[3];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[3];
                break;
            //例外用
            default:
                cup.GetComponent<Renderer>().material.mainTexture = null;
                sotai.GetComponent<Renderer>().material.mainTexture = null;
                Debug.Log("プレイヤー色エラー");
                break;
        }
    }
}
