using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{


    [Header("マップのデータファイル")]
    [SerializeField]
    private TextAsset textFile;

    [SerializeField]
    private TextAsset MapFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int textXNumber; // 行数に相当
    private int textYNumber; // 列数に相当

    bool centerPosRegister = false;//マップのセンターポジションがあるかないか

    [Header("赤いブロック")]
    [SerializeField]
    private GameObject redFloorPrefab;
    [SerializeField]
    private GameObject blueFloorPrefab;
    [SerializeField]
    private GameObject Goal;
    [SerializeField]
    private GameObject centerObject;
    [SerializeField]
    private GameObject GameManager;

    [SerializeField]
    private GameObject Player;

    //センターのポジション決めるための変数達
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 center;

    //Floorのlist
    List<GameObject> mapObjects = new();
    private void Start()
    {
        string textLines = MapFile.text; // テキストの全体データの代入
        //print(textLines);

        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');

        // 行数と列数の取得
        textXNumber = textData[0].Split(',').Length;
        textYNumber = textData.Length;

        // ２次元配列の定義
        dungeonMap = new string[textYNumber, textXNumber];//マップ
        print(dungeonMap);
        Debug.Log("マップ");
        Debug.Log(dungeonMap);

        for (int i = 0; i < textYNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');

            for (int j = 0; j < textXNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];

                if (dungeonMap[i, j] != null)
                {
                    switch (dungeonMap[i, j])
                    {
                        case "0":

                            break;

                        case "1":

                            GameObject floor1 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor1.GetComponent<Floor>().SetMapPosition(i, j, "red");
                            mapObjects.Add(floor1);
                            floor1.GetComponent<Floor>().SetParentmap(this);
                            Debug.Log(i);
                            Debug.Log(j);
                            break;

                        case "2":

                            GameObject floor2 = Instantiate(blueFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor2.GetComponent<Floor>().SetMapPosition(i, j, "blue");
                            mapObjects.Add(floor2);
                            floor2.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case "3":

                            GameObject floor3 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor3.GetComponent<Floor>().SetMapPosition(i, j, "goal");
                            mapObjects.Add(floor3);
                            floor3.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case "4":

                            GameObject floor4 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor4.GetComponent<Floor>().SetMapPosition(i, j, "player");
                            mapObjects.Add(floor4);
                            floor4.GetComponent<Floor>().SetParentmap(this);

                            //プレイヤー生成
                            Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity);


                            break;

                    }
                    if (centerPosRegister == false)//センターポスがなかったら登録する
                    {
                        startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
                        endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

                        center = (startPos + endPos) / 2;

                        Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);


                        centerPosRegister = true;//一つ登録すればOK
                    }
                }
            }
        }

        //Instantiate(GameManager, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);
    }

    private void Update()
    {





    }

    public List<GameObject> GetGameObjectList()
    {

        return mapObjects;
    }
    public void ChangeMap(GameObject obj)//床しか入れん
    {
        //このｆって変数なんなん
        GameObject floor = mapObjects.Find(f => f.gameObject.GetComponent<Floor>() == obj.GetComponent<Floor>());


        float x = floor.GetComponent<Floor>().GetMapPosition().x;
        float z = floor.GetComponent<Floor>().GetMapPosition().z;


        floor.GetComponent<Floor>().SetFloorState(floor.GetComponent<Floor>().GetFloorState());
        Debug.Log(x);
        Debug.Log(z);

    }

    public void CheckMap()
    {

        GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");
        GameObject gola =  mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "goal");

        //フロアの上下上下のフロアに行けるかどうか
        //元居た場所には戻らないようにする(通ってきたフロアの座標をlistで保管するとか)
        //今配置してあるプレイヤーが通れるフロアのlistを作り、つながっているかどうかの判定をするのはどう？
        //配置してあるブロックごとに上と下と左と右のブロックの情報をチェックするやり方はどう？









    }

}
