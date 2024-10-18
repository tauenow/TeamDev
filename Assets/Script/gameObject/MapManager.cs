using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class MapManager : MonoBehaviour
{

    [SerializeField]
    private StageSelectManager parentManager;

    [Header("マップのデータファイル")]
    public TextAsset MapFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int textXNumber; // 行数に相当
    private int textYNumber; // 列数に相当

    bool centerPosRegister = false;//マップのセンターポジションがあるかないか

    [Header("ブロック")]
    [SerializeField]
    private GameObject FloorPrefab_2color;
    [SerializeField]
    private GameObject FloorPrefab_3color;
    [SerializeField]
    private GameObject FloorPrefab_4color;

    //入れるよう
    private GameObject Floor = null;

    [Header("ゴールのオブジェクト")]
    [SerializeField]
    private GameObject Goal;
    [SerializeField]
    private GameObject centerObject;
    [SerializeField]
    private GameObject GameManager;

    //プレイヤーを設定
    [Header("プレイヤーのオブジェクト")]
    [SerializeField]
    private GameObject Player;

    private GameObject playerObject = null;

    //センターのポジション決めるための変数達
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 center;

    //Floorのlist
    private List<GameObject> mapObjects = new();
    //チェックするときの通ったオブジェクトを格納する
    private List<Floor> oldlist = new();

    //マップチェックTime
    private bool check = false;
    private bool mapCheck = false;
    private float mapCheckTime = 0.0f;

    //ゴールのたどり着くための変数
    private bool onGoal;
    private List<Vector3> playerRoot = new();

    private float currentTime = 0.0f;

    //面の数
    private int faceNum = 0;

    private void Start()
    {
        string textLines = MapFile.text; // テキストの全体データの代入
        //print(textLines);

        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');

        // 行数と列数の取得
        textXNumber = textData[0].Split(',').Length;
        textYNumber = textData.Length;
        textYNumber -= 1;
       

        // ２次元配列の定義
        dungeonMap = new string[textYNumber, textXNumber];//マップ

        int state = 0;

        //面の数の指定
        for (int i = 0; i < textYNumber; i++)
        {

            string[] tempWords = textData[i].Split(',');
            for (int j = 0; j < textXNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];

                state = int.Parse(dungeonMap[i, j]);
                if (dungeonMap[i, j] != null)
                {
                    switch (state)//スイッチ文ゴミ
                    {
                        case 0:
                            break;
                        case 1:

                            if (faceNum < 1)
                            {
                                faceNum = 1;
                            }
                            break;
                        case 2:

                            if (faceNum < 2)
                            {
                                faceNum = 2;
                            }
                            break;
                        case 3:

                            if (faceNum < 3)
                            {
                                faceNum = 3;
                            }
                            break;
                        case 4:

                            if (faceNum < 4)
                            {
                                faceNum = 4;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        Debug.Log(faceNum);
        if (faceNum == 2)
        {
            Floor = FloorPrefab_2color;
            Debug.Log("テクスチャを変更");
        }
        if (faceNum == 3)
        {
            Floor = FloorPrefab_3color;
            Debug.Log("テクスチャを変更");
        }
        if (faceNum == 4)
        {
            Floor = FloorPrefab_4color;
            Debug.Log("テクスチャを変更");
        }

        for (int i = 0; i < textYNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');

            for (int j = 0; j < textXNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];

                state = int.Parse(dungeonMap[i, j]);

                if (dungeonMap[i, j] != null)
                {
                    switch (state)//スイッチ文ゴミ
                    {
                        case 0:

                            break;
                        case 1:
                            GameObject floor1 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor1.GetComponent<Floor>().SetMapPosition(j, i, "red");
                            floor1.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor1);
                            floor1.GetComponent<Floor>().SetParentmap(this);

                            break;

                        case 2:
                            GameObject floor2 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor2.GetComponent<Floor>().SetMapPosition(j, i, "blue");
                            floor2.transform.Rotate(90.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor2);
                            floor2.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case 3:
                            GameObject floor3 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor3.GetComponent<Floor>().SetMapPosition(j, i, "yellow");
                            floor3.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor3);
                            floor3.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case 4:
                            GameObject floor4 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor4.GetComponent<Floor>().SetMapPosition(j, i, "green");
                            floor4.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor4);
                            floor4.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case 5:
                            GameObject floor5 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor5.GetComponent<Floor>().SetMapPosition(j, i, "goal");
                            mapObjects.Add(floor5);
                            floor5.GetComponent<Floor>().SetParentmap(this);


                            break;
                        case 6:
                            GameObject floor6 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor6.GetComponent<Floor>().SetMapPosition(j, i, "player");
                            floor6.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor6);
                            floor6.GetComponent<Floor>().SetParentmap(this);

                            //プレイヤー生成
                            playerObject = Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity) as GameObject;
                            playerObject.GetComponent<PlayerControl>().SetMapPosition(floor6.GetComponent<Floor>().GetMapPosition());

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        Debug.Log("センターを登録");
        if (centerPosRegister == false)//センターポスがなかったら登録する
        {
            startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

            center = (startPos + endPos) / 2;

            Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);

            centerPosRegister = true;//一つ登録すればOK
        }

    }

    private void Update()
    {

        if(onGoal == true)
        {
            //いらないルートを消す
            for(int i = 0;i < oldlist.Count;i++)
            {
                if (oldlist[i].GetRootCount() == 0)
                {
                    oldlist[i].CheckOldRoot();
                }
            }
            //プレイヤーのポジションからゴールのルートまでのrootのpositionを格納
            for (int i = 0; i < oldlist.Count; i++)
            {
                if (oldlist[i].GetRootCount() != 0)
                {
                    playerRoot.Add(oldlist[i].GetMapPosition());
                }
            }
            //ゴールのpositionも格納
            Floor goal = oldlist.Find(match => match.GetFloorState() == "goal");
            playerRoot.Add(goal.GetMapPosition());

            //プレイヤーのポジションに近い順にソート
            playerRoot.Sort((a, b) => Mathf.Sqrt(a.x + a.x * a.z + a.z).CompareTo(Mathf.Sqrt(b.x + b.x * b.z + b.z)));

            List<int> rootNumber = new List<int>();

            for(int i = 0;i<playerRoot.Count;i++)
            {
                if(i != 0&&i != playerRoot.Count)
                {
                    if (playerRoot[i].x == playerRoot[i - 1].x && playerRoot[i].z == playerRoot[i - 1].z)
                    {
                        playerRoot.RemoveAt(i);
                    }
                }
            } 

            //プレイヤーが通るルートを格納&&プレイヤーがゴールまで動くのを許可
            playerObject.GetComponent<PlayerControl>().SetGoalRoot(playerRoot);
            playerObject.GetComponent<PlayerControl>().OnPlayerMove();
            //ゴールしたらいじれんようにする
            GetComponent<CursorManager>().onGoal = true;
            //一回はいればよくね？
            
            onGoal = false;
        }

        if (check == true)
        {
            mapCheckTime += Time.deltaTime;
        }
        if (mapCheckTime >= 1.0f)
        {
            if (mapCheck == true)
            {
                CheckMap();
                mapCheck = false;
            }
        }
        if (mapCheckTime >= 1.5f)
        {
            mapCheckTime = 0.0f;
            check = false;
            CursorManager.floorChange = false;
        }

    }

    public List<GameObject> GetGameObjectList()
    {

        return mapObjects;
    }
    public void ChangeMap(GameObject obj)//床しか入れん
    {
        
        //このｆって変数なんなん
        GameObject floor = mapObjects.Find(f => f.gameObject.GetComponent<Floor>() == obj.GetComponent<Floor>());

        oldlist.Clear();
        float x = floor.GetComponent<Floor>().GetMapPosition().x;
        float z = floor.GetComponent<Floor>().GetMapPosition().z;

        floor.GetComponent<Floor>().SetFloorState(floor.GetComponent<Floor>().GetFloorState());
        
        obj.GetComponent<Floor>().OnChange();

        check = true;
        mapCheck = true;

    }

    public void CheckMap()
    {

        GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");
        
        oldlist.Clear();

        oldlist.Add(player.GetComponent<Floor>());
        player.GetComponent<Floor>().CheckFloor();

        //フロアの上下上下のフロアに行けるかどうか
        //元居た場所には戻らないようにする(通ってきたフロアの座標をlistで保管するとか)
        //今配置してあるプレイヤーが通れるフロアのlistを作り、つながっているかどうかの判定をするのはどう？
        //配置してあるブロックごとに上と下と左と右のブロックの情報をチェックするやり方はどう？ ←採用

    }

    public void linkChangeFloor(GameObject gameObject)
    {
        GameObject obj_top = null;
        GameObject obj_bottom = null;
        GameObject obj_left = null;
        GameObject obj_right = null;

        //↑
        obj_top = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z - 1);
        //↓
        obj_bottom = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z + 1);
        //←
        obj_left = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);
        //→
        obj_right = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);


        if (obj_top != null) if (obj_top.GetComponent<Floor>().GetFloorState() == "player") obj_top = null;
        if (obj_bottom != null) if (obj_bottom.GetComponent<Floor>().GetFloorState() == "player") obj_bottom = null;
        if (obj_left != null) if (obj_left.GetComponent<Floor>().GetFloorState() == "player") obj_left = null;
        if (obj_right != null) if (obj_right.GetComponent<Floor>().GetFloorState() == "player") obj_right = null;



        if (obj_top != null)
        {
            obj_top.GetComponent<Floor>().LinkChange();
            obj_top.GetComponent<Floor>().SetFloorState(obj_top.GetComponent<Floor>().GetFloorState());
        }
        if (obj_bottom != null)
        {
            obj_bottom.GetComponent<Floor>().LinkChange();
            obj_bottom.GetComponent<Floor>().SetFloorState(obj_bottom.GetComponent<Floor>().GetFloorState());
        }
        if (obj_left != null)
        {
            obj_left.GetComponent<Floor>().LinkChange();
            obj_left.GetComponent<Floor>().SetFloorState(obj_left.GetComponent<Floor>().GetFloorState());
        }
        if (obj_right != null)
        {
            obj_right.GetComponent<Floor>().LinkChange();
            obj_right.GetComponent<Floor>().SetFloorState(obj_right.GetComponent<Floor>().GetFloorState());
        }

    }

    public List<Floor> GetOldList()
    {
        return oldlist;
    }
    public void InGoal()
    {
        onGoal = true;
    }
    public bool GetIsGoal()
    {
        return onGoal;
    }

    public int GetFaceNum()
    {
        return faceNum;
    }

}
