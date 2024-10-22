using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    //スクリプタぶるオブジェクトのClearは-1して
    //ステージセレクトから何番目のステージを選んだかを入れる変数
    [SerializeField] private StageScriptableObject StageObj;
    private int selecetStageNum = 1;
    [SerializeField] private GameObject map;
    //マップファイル一覧
    [SerializeField]
    private TextAsset MapFile1;
    List<TextAsset> mapFaileList = new();

    [Header("フェード")]
    [SerializeField] private Image fade = default;

    private GameObject mapObject;
    //このステージをクリアしたかどうか
    public bool isClear = false;

    void Start()
    {
        //リストから呼び出すときは-1してね
        //テクストファイルのリストにマップのデータを全て格納する
        mapFaileList.Add(MapFile1);

        if(selecetStageNum == 1)
        {
            mapObject = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            mapObject.GetComponent<MapManager>().MapFile = MapFile1;
            mapObject.GetComponent<MapManager>().parentManager = this;
        }

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
