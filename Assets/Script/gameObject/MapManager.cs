using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapManager : MonoBehaviour
{
	[SerializeField] private StageScriptableObject StageNum;
	private int Stage;

	[Header("マップのデータファイル")]
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
	//チェックするときの通ったオブジェクトを格納する
	List<Floor> oldlist = new();

	//出したブロックの最小座標と最大座標を残しとく
	private float minPosX = 0.0f;
	private float minPosZ = 0.0f;
	private float maxPosX = 0.0f;
	private float maxPosZ = 0.0f;

	private void Start()
	{
		Stage = StageNum.StageNum;
		Debug.Log(Stage);

		string textLines = MapFile.text; // テキストの全体データの代入
		print(textLines);

		// 改行でデータを分割して配列に代入
		textData = textLines.Split('\n');

		// 行数と列数の取得
		textXNumber = textData[0].Split(',').Length;
		textYNumber = textData.Length;

		textYNumber -= 1;


		// ２次元配列の定義
		dungeonMap = new string[textYNumber, textXNumber];//マップ

		//Debug.Log("マップ");
		Debug.Log("X" + textXNumber);
		//Debug.Log(textYNumber);
		int state = 0;

		for (int i = 0; i < textYNumber; i++)
		{
			string[] tempWords = textData[i].Split(',');

			for (int j = 0; j < textXNumber; j++)
			{
				dungeonMap[i, j] = tempWords[j];

				Debug.Log(i + "," + j + "=" + dungeonMap[i, j]);

				state = int.Parse(dungeonMap[i, j]);

				if (j == 6 && i == 0)
				{
					Debug.Log("でてます" + state);
				}

				if (dungeonMap[i, j] != null)
				{
					switch (state)//スイッチ文ゴミ
					{
						case 0:

							break;
						case 1:
							if (j == 6 && i == 0)
							{
								Debug.Log("でてます" + dungeonMap[i, j]);
							}

							GameObject floor1 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor1.GetComponent<Floor>().SetMapPosition(i, j, "red");
							mapObjects.Add(floor1);
							floor1.GetComponent<Floor>().SetParentmap(this);

							break;

						case 2:

							GameObject floor2 = Instantiate(blueFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor2.GetComponent<Floor>().SetMapPosition(i, j, "blue");
							mapObjects.Add(floor2);
							floor2.GetComponent<Floor>().SetParentmap(this);

							break;
						case 3:

							GameObject floor3 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor3.GetComponent<Floor>().SetMapPosition(i, j, "goal");
							mapObjects.Add(floor3);
							floor3.GetComponent<Floor>().SetParentmap(this);

							break;
						case 4:

							GameObject floor4 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor4.GetComponent<Floor>().SetMapPosition(i, j, "player");
							mapObjects.Add(floor4);
							floor4.GetComponent<Floor>().SetParentmap(this);

							Debug.Log("プレイヤー生成");
							//プレイヤー生成
							Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity);

							break;
						default:
							break;
					}
				}
			}
		}
		if (centerPosRegister == false)//センターポスがなかったら登録する
		{
			startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
			endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

			center = (startPos + endPos) / 2;

			Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);

			centerPosRegister = true;//一つ登録すればOK
		}


		//マップの一番最初のオブジェクトの座標と一番最後のオブジェクトの座標を格納
		minPosX = mapObjects.First().GetComponent<Floor>().GetMapPosition().x;
		minPosZ = mapObjects.First().GetComponent<Floor>().GetMapPosition().z;
		maxPosX = mapObjects.Last().GetComponent<Floor>().GetMapPosition().x;
		maxPosZ = mapObjects.Last().GetComponent<Floor>().GetMapPosition().z;

		//Debug.Log("min" + minPosX);

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

		obj.GetComponent<ChangeFloor>().OnChange();
		Debug.Log(x);
		Debug.Log(z);

		CheckMap();

	}

	public void CheckMap()
	{



		GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");
		GameObject gola = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "goal");
		oldlist.Clear();

		Debug.Log(player.GetComponent<Floor>().GetMapPosition().x);

		player.GetComponent<Floor>().CheckFloor(oldlist);

		//フロアの上下上下のフロアに行けるかどうか
		//元居た場所には戻らないようにする(通ってきたフロアの座標をlistで保管するとか)
		//今配置してあるプレイヤーが通れるフロアのlistを作り、つながっているかどうかの判定をするのはどう？
		//配置してあるブロックごとに上と下と左と右のブロックの情報をチェックするやり方はどう？ ←採用



	}

	public float MinPositionX()
	{
		return minPosX;
	}
	public float MinPositionZ()
	{
		return minPosZ;
	}
	public float MaxPositionX()
	{
		return maxPosX;
	}
	public float MaxPositionZ()
	{
		return maxPosZ;
	}
}
