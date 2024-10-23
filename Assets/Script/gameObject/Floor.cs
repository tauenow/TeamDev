using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public bool Mouse = false;
	public bool Tap = false;

	//Floorの変数
	private Vector3 position;//このFloorの2次元配列のポジション
	private string state;//２次元配列で登録されている文字ナンバー
	private MapManager parentMap = null;
	private Floor oldFloor = null;//このFloorのチェックに入る前の情報
	private int rootCount = 0;//このFloorからの分岐の数

	//ChangeFloorの変数
	[SerializeField]
	private float motionFrame = 0.1f;
	private int motionCount = 0;
	private float currentTime = 0.0f;

	private int changeMotionCount = 20;

	private bool changeWait = false;
	private bool change = false; //床が変わる
	private float currentLinkTime = 0.0f;
	private int motionLinkCount = 0;
	private bool linkChange = false;

	[SerializeField]
	private float Hight;
	//カーソルが当たっているか
	private bool cursor = false;
	//色の数
	private int faceCount = 1;
	//色情報を変える
	private bool link = true;

	//選んでるときは色を発行させる
	[SerializeField]
	private float blockPlayerEmissive = 0.0f;
	[SerializeField]
	private float blockEmissive = 0.0f;

	[Header("共有オブジェクト")][SerializeField] private StageScriptableObject scriptableObject;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		ChangeFloor();
		if (Tap == true) TouchUpdate();
		if (Mouse == true) CursorUpdate();

		LinkChangeFloor();

	}

	private void ChangeFloor()
	{
		if (change == true)//床の色を変えるモーション
		{
			//一回しか入らないようにする
			if (link == true)
			{
				Debug.Log("周りも変えるで");
				GameObject obj = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == position.x && match.GetComponent<Floor>().GetMapPosition().z == position.z);
				//周辺の色情報を変更
				parentMap.LinkChangeFloor(obj);
				link = false;
			}
			currentTime += Time.deltaTime;

			//ここら辺のモーションは変えたほうが良い
			if (motionCount < changeMotionCount * 3)
			{
				if (currentTime >= motionFrame)
				{
					//2面と4面の時は通常
					if (parentMap.GetFaceNum() == 2 || parentMap.GetFaceNum() == 4)
					{
						if (motionCount < changeMotionCount)
						{
							Vector3 transformPos = transform.position;
							transformPos.y += 0.1f;
							transform.position = transformPos;
						}
						else if (motionCount < changeMotionCount * 2)
						{
							transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);

						}
						else if (motionCount < changeMotionCount * 3)
						{
							Vector3 transformPos = transform.position;
							transformPos.y -= 0.1f;
							transform.position = transformPos;
						}
						motionCount++;
						currentTime = 0.0f;
					}
					//3面の時
					else if (parentMap.GetFaceNum() == 3)
					{
						Debug.Log("3色");
						if (motionCount < changeMotionCount)
						{
							Vector3 transformPos = transform.position;
							transformPos.y += 0.1f;
							transform.position = transformPos;
						}
						else if (motionCount < changeMotionCount * 2)
						{
							if (faceCount >= 4)
							{
								Debug.Log("z軸");
								transform.Rotate(0.0f, 0.0f, 90.0f * 1.0f / changeMotionCount);
							}
							else if (faceCount >= 1)
							{
								Debug.Log("x軸");
								transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
							}

						}
						else if (motionCount < changeMotionCount * 3)
						{
							Vector3 transformPos = transform.position;
							transformPos.y -= 0.1f;
							transform.position = transformPos;
						}
						motionCount++;
						currentTime = 0.0f;
					}

				}
			}
		}
		if (motionCount >= changeMotionCount * 3)
		{

			GameObject obj = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == position.x && match.GetComponent<Floor>().GetMapPosition().z == position.z);

			if (obj != null)
			{
				parentMap.LinkChangeFloorMotion(obj);
				link = true;
			}
			if (obj == null)
			{
				Debug.Log("中身ないよ");
			}
			if (parentMap.GetFaceNum() == 3)
			{
				if (faceCount >= 4)
				{
					transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
					faceCount = 1;
				}
			}
			//光度変更
			if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
			else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;

			motionCount = 0;
			currentTime = 0.0f;
			changeWait = false;
			change = false;

		}
	}
	//マウスでの操作
	private void CursorUpdate()
	{
		if (change == false)//カーソルが当たっている時のモーション
		{
			if (linkChange == false)
			{
				if (cursor == true)
				{
					Vector3 pos = transform.position;
					pos.y = 0.2f;//マジックナンバーごめん
					transform.position = pos;
					cursor = false;
					return;
				}
				else if (cursor == false)
				{
					Vector3 pos = transform.position;
					pos.y = 0.0f;//マジックナンバーごめん
					transform.position = pos;

				}
			}
		}

	}
	//スマホタップでの操作
	private void TouchUpdate()
	{
		if (change == false)//カーソルが当たっている時のモーション
		{
			if (linkChange == false)
			{
				if (changeWait == true)
				{
					Vector3 pos = transform.position;
					pos.y = 0.3f;//マジックナンバーごめん
					transform.position = pos;
					return;
				}
				else if (changeWait == false)
				{
					Vector3 pos = transform.position;
					pos.y = 0.0f;//マジックナンバーごめん
					transform.position = pos;

				}
			}
		}
	}
	private void LinkChangeFloor()
	{
		//このオブジェクトの周りも変える
		if (linkChange == true)
		{
			currentLinkTime += Time.deltaTime;

			if (currentLinkTime > motionFrame)
			{
				//2面と4面の時は通常
				if (parentMap.GetFaceNum() == 2 || parentMap.GetFaceNum() == 4)
				{
					if (motionLinkCount >= changeMotionCount)
					{
						linkChange = false;
						currentLinkTime = 0.0f;
						motionLinkCount = 0;
					}
					else if (motionLinkCount < changeMotionCount)
					{
						transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
						currentLinkTime = 0.0f;
						motionLinkCount++;
						//光度変更
						if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
						else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;
					}
				}
				//3面の時
				else if (parentMap.GetFaceNum() == 3)
				{
					if (motionLinkCount >= changeMotionCount)
					{
						linkChange = false;
						currentLinkTime = 0.0f;
						motionLinkCount = 0;
						if (faceCount >= 4)
						{
							transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
							faceCount = 1;
						}
						//光度変更
						if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
						else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;
					}
					else if (motionLinkCount < changeMotionCount)
					{
						if (faceCount >= 4)
						{
							Debug.Log("z軸");
							transform.Rotate(0.0f, 0.0f, 90.0f * 1.0f / changeMotionCount);
						}
						else if (faceCount >= 1)
						{
							Debug.Log("x軸");
							transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
						}
						currentLinkTime = 0.0f;
						motionLinkCount++;
					}
				}

			}


		}
	}

	//Floorの関数
	public void SetMapPosition(float posX, float posZ, string num)
	{

		position.x = posX;
		position.z = posZ;
		state = num;

		//光度変更
		if (state == "player") GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
		else if (state == "goal") GetComponent<MeshRenderer>().material.color = Color.white;
		else if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
		else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;

	}
	public void SetFloorState(string num)
	{

		if (parentMap.GetFaceNum() == 2)//二色の場合
		{
			if (num == "red")
			{
				state = "blue";
			}
			else if (num == "blue")
			{
				state = "red";
			}
		}
		if (parentMap.GetFaceNum() == 3)//三色の場合
		{
			if (num == "red")
			{
				state = "blue";
			}
			else if (num == "blue")
			{
				state = "yellow";
			}
			else if (num == "yellow")
			{
				state = "red";
			}
		}
		if (parentMap.GetFaceNum() == 4)//四色の場合
		{
			if (num == "red")
			{
				state = "blue";
			}
			else if (num == "blue")
			{
				state = "yellow";
			}
			else if (num == "yellow")
			{
				state = "green";
			}
			else if (num == "green")
			{
				state = "red";
			}
		}

	}

	public Vector3 GetMapPosition()
	{

		return position;

	}

	public string GetFloorState()
	{
		return state;
	}

	public void SetParentmap(MapManager map)
	{
		parentMap = map;
	}
	public void SetRootCount(int count)
	{
		rootCount = count;
	}
	public int GetRootCount()
	{
		return rootCount;
	}

	public void SetOldFloor(Floor floor)
	{
		oldFloor = floor;
	}
	public Floor GetOldFloor()
	{
		return oldFloor;
	}
	public void SetFaceCount(int count)
	{
		faceCount = count;
	}
	public int GetFaceCount()
	{
		return faceCount;
	}

	public void CheckFloor()
	{
		rootCount = 0;
		List<GameObject> objList = new();

		GameObject obj1 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z - 1);
		GameObject obj2 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == (GetComponent<Floor>().GetMapPosition().z + 1));
		GameObject obj3 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
		GameObject obj4 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);


		//Debug.Log("↑を調べます");

		if (obj1 != null)
		{
			if (parentMap.GetOldList().Contains(obj1.GetComponent<Floor>()) == false)
			{

				//Debug.Log("探索してきたlistの中になかったのでチェックします"
				if (obj1.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
				{
					parentMap.GetOldList().Add(obj1.GetComponent<Floor>());//チェックしたFloorはlistに登録

					//Debug.Log("↑は赤です");
					//rootの数と自分の情報を登録                                                      
					rootCount++;
					obj1.GetComponent<Floor>().SetOldFloor(this);
					objList.Add(obj1);


				}
				else if (obj1.GetComponent<Floor>().GetFloorState() == "goal")
				{
					parentMap.GetOldList().Add(obj1.GetComponent<Floor>());//チェックしたFloorはlistに登録
					Debug.Log("goalです");
					rootCount++;
					//ゴールしたことをマップマネージャーに伝える
					parentMap.InGoal();
				}
			}
		}
		//Debug.Log("↓を調べます");
		if (obj2 != null)
		{
			if (parentMap.GetOldList().Contains(obj2.GetComponent<Floor>()) == false)
			{

				//Debug.Log("探索してきたlistの中になかったのでチェックします");

				if (obj2.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
				{
					parentMap.GetOldList().Add(obj2.GetComponent<Floor>());//チェックしたFloorはlistに登録

					//Debug.Log("↓は赤です");
					rootCount++;
					obj2.GetComponent<Floor>().SetOldFloor(this);
					objList.Add(obj2);
				}
				else if (obj2.GetComponent<Floor>().GetFloorState() == "goal")
				{
					parentMap.GetOldList().Add(obj2.GetComponent<Floor>());//チェックしたFloorはlistに登録
					Debug.Log("goalです");
					rootCount++;
					//ゴールしたことをマップマネージャーに伝える
					parentMap.InGoal();

				}

			}
		}

		//Debug.Log("←を調べます");
		if (obj3 != null)
		{
			if (parentMap.GetOldList().Contains(obj3.GetComponent<Floor>()) == false)
			{

				//Debug.Log("探索してきたlistの中になかったのでチェックします");

				if (obj3.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
				{
					parentMap.GetOldList().Add(obj3.GetComponent<Floor>());//チェックしたFloorはlistに登録


					//Debug.Log("←は赤です");
					//rootの数と自分の情報を登録
					rootCount++;
					obj3.GetComponent<Floor>().SetOldFloor(this);
					objList.Add(obj3);
				}
				else if (obj3.GetComponent<Floor>().GetFloorState() == "goal")
				{
					parentMap.GetOldList().Add(obj3.GetComponent<Floor>());//チェックしたFloorはlistに登録
					Debug.Log("goalです");
					rootCount++;
					//ゴールしたことをマップマネージャーに伝える
					parentMap.InGoal();

				}

			}
		}


		//Debug.Log("→を調べます");
		if (obj4 != null)
		{
			if (parentMap.GetOldList().Contains(obj4.GetComponent<Floor>()) == false)
			{
				//探索していたlistの中になければ進む
				if (obj4.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
				{
					parentMap.GetOldList().Add(obj4.GetComponent<Floor>());//チェックしたFloorはlistに登録

					//Debug.Log("→は赤です");
					//rootの数と自分の情報を登録
					rootCount++;
					obj4.GetComponent<Floor>().SetOldFloor(this);
					objList.Add(obj4);

				}
				else if (obj4.GetComponent<Floor>().GetFloorState() == "goal")
				{
					parentMap.GetOldList().Add(obj4.GetComponent<Floor>());//チェックしたFloorはlistに登録
					Debug.Log("goalです");
					rootCount++;
					//ゴールしたことをマップマネージャーに伝える
					parentMap.InGoal();

				}

			}
		}

		Debug.Log(position.x);
		Debug.Log(position.z);

		if (objList.Count == 1)
		{
			StartCoroutine(objList[objList.Count - 1].GetComponent<Floor>().Check());
		}
		else if (objList.Count == 2)
		{
			StartCoroutine(objList[objList.Count - 1].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 2].GetComponent<Floor>().Check());

		}
		else if (objList.Count == 3)
		{
			StartCoroutine(objList[objList.Count - 1].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 2].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 3].GetComponent<Floor>().Check());

		}
		else if (objList.Count == 4)
		{
			StartCoroutine(objList[objList.Count - 1].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 2].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 3].GetComponent<Floor>().Check());
			StartCoroutine(objList[objList.Count - 4].GetComponent<Floor>().Check());

		}
	}

	public IEnumerator Check()
	{
		yield return new WaitForSeconds(0.002f);
		CheckFloor();
	}
	public void CheckOldRoot()
	{

		//rootが一つもなかったら前の情報のrootも確認
		if (rootCount == 0)
		{
			Debug.Log("戻ります");
			if (oldFloor != null) oldFloor.CheckOldRoot();

			Debug.Log(position.x);
			Debug.Log(position.z);
		}
		else if (rootCount >= 0)
		{
			rootCount--;
			if (rootCount == 0)
			{
				Debug.Log("戻ります");
				if (oldFloor != null) oldFloor.CheckOldRoot();
				Debug.Log(position.x);
				Debug.Log(position.z);
			}
		}

	}

	//ChangeFloor
	public void LinkChange()
	{
		linkChange = true;
	}

	public void OnChange()
	{
		change = true;
		currentTime = 0.0f;

	}
	public bool GetChangeState()
	{
		return change;
	}
	public void OnCursor()
	{
		cursor = true;
	}

	public void SetChangeWait(bool value)
	{
		changeWait = value;
	}
	public bool GetChangeWait()
	{
		return changeWait;
	}

}
