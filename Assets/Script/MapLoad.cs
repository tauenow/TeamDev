using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Map_Load : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int textXNumber; // 行数に相当
    private int textYNumber; // 列数に相当

    bool centerPosRegister = false;//マップのセンターポジションがあるかないか
 
    [SerializeField]
    private GameObject redFloorPrefab;
    [SerializeField]
    private GameObject blueFloorPrefab;
    [SerializeField]
    private GameObject centerObject;
    [SerializeField]
    private GameObject GameManager;

    //センターのポジション決めるための変数達
    private Vector3 startPos;  
    private Vector3 endPos;
    private Vector3 center;

    //Floorのlist

    private void Start()
    {
        string textLines = textFile.text; // テキストの全体データの代入
        print(textLines);

        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');

        // 行数と列数の取得
        textXNumber = textData[0].Split(',').Length;
        textYNumber = textData.Length;

        // ２次元配列の定義
        dungeonMap = new string[textYNumber, textXNumber];//マップ

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
                           
                            Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity);
                            
                            break;

                        case "2":

                            Instantiate(blueFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity);
                            break;
                            
                    }
                    if(centerPosRegister == false)//センターポスがなかったら登録する
                    {
                        startPos = new  Vector3(transform.position.x,0.0f, transform.position.z);
                        endPos = new Vector3(transform.position.x + (textXNumber - 1),0.0f, transform.position.z - (textYNumber - 1));

                        center = (startPos + endPos) / 2;
                        Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);

                        centerPosRegister = true;//一つ登録すればOK
                    }
                }
            }
        }

        Instantiate(GameManager, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);
    }

    private void Update()
    {
       




    }
}
