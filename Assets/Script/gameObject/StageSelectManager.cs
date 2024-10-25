using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    //スクリプタぶるオブジェクトのClearは-1して
    //ステージセレクトから何番目のステージを選んだかを入れる変数
    [SerializeField] private StageScriptableObject StageObj;
    private int selecetStageNum = 0;
    [SerializeField] private GameObject map;
    //マップファイル一覧
    [Header("マップファイル一覧")]
    [SerializeField]
    private TextAsset TutorialMap;
    [SerializeField]
    private TextAsset MapFile1;
    [SerializeField]
    private TextAsset MapFile2;
    [SerializeField]
    private TextAsset MapFile3;
    [SerializeField]
    private TextAsset MapFile4;
    [SerializeField]
    private TextAsset MapFile5;
    [SerializeField]
    private TextAsset MapFile6;
    [SerializeField]
    private TextAsset MapFile7;
    [SerializeField]
    private TextAsset MapFile8;
    [SerializeField]
    private TextAsset MapFile9;

    private readonly List<TextAsset> mapFaileList = new();

    [Header("フェード")]
    [SerializeField] private Image fade = default;

    private GameObject mapObject;
    //このステージをクリアしたかどうか
    public bool isClear = false;

    void Start()
    {

        mapObject = null;
        isClear = false;

        selecetStageNum = 0;

        selecetStageNum = StageObj.StageNum;

        //リストから呼び出すときは-1してね
        //テクストファイルのリストにマップのデータを全て格納する
        mapFaileList.Add(MapFile1);
        mapFaileList.Add(MapFile2);
        mapFaileList.Add(MapFile3);
        mapFaileList.Add(MapFile4);
        mapFaileList.Add(MapFile5);
        mapFaileList.Add(MapFile6);
        mapFaileList.Add(MapFile7);
        mapFaileList.Add(MapFile8);
        mapFaileList.Add(MapFile9);

       

        mapObject = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        mapObject.GetComponent<MapManager>().MapFile = mapFaileList[selecetStageNum - 1];
        mapObject.GetComponent<MapManager>().parentManager = this;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoneGoal()
    {

    }
    public GameObject GetMapObject()
    {
        return mapObject;
    }
    public void ChangeScene()
    {
        StageObj.isClearList[selecetStageNum - 1] = true;
        fade.GetComponent<FadeINOUT>().FadeToChangeScene();
    }
}
