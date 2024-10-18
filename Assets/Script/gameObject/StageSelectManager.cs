using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private GameObject mapObject;
    //このステージをクリアしたかどうか
    private bool isClear = false;

    void Start()
    {
        StageObj.isClearList.Add(false);
        //selecetStageNum = StageObj.StageNum;

        //ここを量産すればできるで
        if(selecetStageNum == 1)
        {
            mapObject = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            mapObject.GetComponent<MapManager>().MapFile = MapFile1;
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
}
