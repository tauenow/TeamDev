using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using System.ComponentModel.Design;


public class MapManager : MonoBehaviour
{
	public StageSelectManager parentManager;

	[Header("�}�b�v�̃f�[�^�t�@�C��")]
	public TextAsset MapFile;

	private string[] textData;
	private string[,] dungeonMap;

	private int textXNumber; // �s���ɑ���
	private int textYNumber; // �񐔂ɑ���

	bool centerPosRegister = false;//�}�b�v�̃Z���^�[�|�W�V���������邩�Ȃ���

	[Header("�}�e���A��")]
	[SerializeField]
	private Material color_2;
	[SerializeField]
	private Material color_3;
	[SerializeField]
	private Material color_4;

	//�����悤
	[SerializeField]
	private GameObject Floor = null;

	[Header("�S�[���̃I�u�W�F�N�g")]
	[SerializeField]
	private GameObject Goal;
	[SerializeField]
	private GameObject centerObject;
	[SerializeField]
	private GameObject GameManager;

	//�v���C���[��ݒ�
	[Header("�v���C���[�̃I�u�W�F�N�g")]
	[SerializeField]
	private GameObject Player;

	private GameObject playerObject = null;

	//�Z���^�[�̃|�W�V�������߂邽�߂̕ϐ��B
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 center;

	//Floor��list
	private List<GameObject> mapObjects;
	//�`�F�b�N����Ƃ��̒ʂ����I�u�W�F�N�g���i�[����
	private List<Floor> checkedFloorList;

	//�����݂�ȋC��t����
	public static bool floorChange = false;

	//�}�b�v�`�F�b�NTime
	private bool isOff = false;
	private float waitTime = 0.0f;

	//�S�[���̂��ǂ蒅�����߂̕ϐ�
	private bool isMapClear;//�}�b�v���N���A�������̔���
	private bool onGoal;//�S�[���ɍs���v���C���[�̃��[�V������On(���I��off�ɂ͂��Ȃ�)
	private List<Vector3> playerRoot;

	//�ʂ̐�
	private int faceNum = 0;

	//�S�[���G�t�F�N�g�I�u�W�F�N�g
	[SerializeField]
	private GameObject goalEffect = null;
	[SerializeField]
	private GameObject playerRootEffect = null;

	[Header("���ʃf�[�^")]
	//���ʃf�[�^
	[SerializeField]
	private StageScriptableObject scriptableObject;

	[Header("�S�[���̃G�t�F�N�g���o������")]
	[SerializeField]
	private float effectCreateTime = 0.2f;

	[Header("�N���A�̑҂���")]
	[SerializeField]
	private float clearwaitTime = 1.0f;

	private void Start()
	{
		//������
		mapObjects = new();
		checkedFloorList = new();
		floorChange = false;

		playerObject = null;
		isOff = false;
		isMapClear = false;
		onGoal = false;
		playerRoot = new();

		faceNum = 0;

		string textLines = MapFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��

		// ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
		textData = textLines.Split('\n');

		// �s���Ɨ񐔂̎擾
		textXNumber = textData[0].Split(',').Length;
		textYNumber = textData.Length;
		textYNumber -= 1;

		// �Q�����z��̒�`
		dungeonMap = new string[textYNumber, textXNumber];//�}�b�v
		int state = 0;

		Debug.Log(scriptableObject.colorName);

		for (int i = 0; i < 1; i++)
		{
			string[] tempWords = textData[i].Split(',');
			for (int j = 1; j < 2; j++)
			{
				dungeonMap[i, j] = tempWords[j];

				faceNum = int.Parse(dungeonMap[i, j]);
			}
		}

		Debug.Log(faceNum);

		if (faceNum == 2)
		{
			Floor.GetComponent<MeshRenderer>().material = color_2;
			Debug.Log("2�F�ł�");
		}
		if (faceNum == 3)
		{
			Floor.GetComponent<MeshRenderer>().material = color_3;
			Debug.Log("3�F�ł�");
		}
		if (faceNum == 4)
		{
			Floor.GetComponent<MeshRenderer>().material = color_4;
			Debug.Log("4�F");
		}

		for (int i = 1; i < textYNumber; i++)
		{
			string[] tempWords = textData[i].Split(',');

			for (int j = 0; j < textXNumber; j++)
			{
				dungeonMap[i, j] = tempWords[j];

				state = int.Parse(dungeonMap[i, j]);

				if (dungeonMap[i, j] != null)
				{
					switch (state)//�X�C�b�`���S�~
					{
						case 0:

							break;
						case 1:
							GameObject floor1 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor1.GetComponent<Floor>().SetParentmap(this);
							floor1.GetComponent<Floor>().SetMapPosition(j, i - 1, "red");
							floor1.transform.Rotate(180.0f, 0.0f, 0.0f);
							floor1.GetComponent<Floor>().SetFaceCount(1);
							mapObjects.Add(floor1);


							break;

						case 2:
							GameObject floor2 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor2.GetComponent<Floor>().SetParentmap(this);
							floor2.GetComponent<Floor>().SetMapPosition(j, i - 1, "blue");
							floor2.transform.Rotate(270.0f, 0.0f, 0.0f);
							floor2.GetComponent<Floor>().SetFaceCount(2);
							mapObjects.Add(floor2);


							break;
						case 3:
							GameObject floor3 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor3.GetComponent<Floor>().SetParentmap(this);
							floor3.GetComponent<Floor>().SetMapPosition(j, i - 1, "yellow");
							floor3.transform.Rotate(0.0f, 0.0f, 0.0f);
							floor3.GetComponent<Floor>().SetFaceCount(3);
							mapObjects.Add(floor3);


							break;
						case 4:
							GameObject floor4 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor4.GetComponent<Floor>().SetParentmap(this);
							floor4.GetComponent<Floor>().SetMapPosition(j, i - 1, "green");
							floor4.transform.Rotate(90.0f, 0.0f, 0.0f);
							mapObjects.Add(floor4);


							break;
						case 5:
							GameObject floor5 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor5.GetComponent<Floor>().SetParentmap(this);
							floor5.GetComponent<Floor>().SetMapPosition(j, i - 1, "goal");
							mapObjects.Add(floor5);


							break;
						case 6:
							GameObject floor6 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor6.GetComponent<Floor>().SetParentmap(this);
							floor6.GetComponent<Floor>().SetMapPosition(j, i - 1, "player");
							if (scriptableObject.colorName == "red") floor6.transform.Rotate(180.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "blue") floor6.transform.Rotate(270.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "yellow") floor6.transform.Rotate(0.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "green") floor6.transform.Rotate(90.0f, 0.0f, 0.0f);

							mapObjects.Add(floor6);

							//�v���C���[����
							playerObject = Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 0.5f, transform.position.z - i), Quaternion.identity) as GameObject;
							playerObject.GetComponent<PlayerControl>().SetMapPosition(floor6.GetComponent<Floor>().GetMapPosition());

							break;
						default:
							break;
					}
				}
			}
		}

		if (centerPosRegister == false)//�Z���^�[�|�X���Ȃ�������o�^����
		{
			startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
			endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

			center = (startPos + endPos) / 2;

			GameObject centerObj = Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity) as GameObject;

			centerPosRegister = true;//��o�^�����OK
			Debug.Log("�Z���^�[��o�^");
			GameObject.Find("Main Camera").GetComponent<CameraControl>().CentrCretae(centerObj);//�J�����̃Z���^�[�ɂȂ�I�u�W�F�N�g��������
		}

		//�v���C���[���S�[���������悤�ɂ���
		GameObject goalObj = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "goal");

		Vector3 transformLookPos = goalObj.transform.position;
		transformLookPos.y += 1;

		playerObject.transform.LookAt(transformLookPos);
	}

	private void Update()
	{

		if (onGoal == true)
		{
			List<Floor> goalRootFloor = new();
			parentManager.isClear = true;
			Debug.Log(parentManager.isClear);
			//����Ȃ����[�g������
			foreach (Floor floor in checkedFloorList)
			{
				if (floor.GetRootCount() == 0)
				{
					floor.CheckOldRoot();
					Debug.Log("���[�g����");
				}
			}
			//�v���C���[�̃|�W�V��������S�[���̃��[�g�܂ł�root��position���i�[
			foreach (Floor floor in checkedFloorList)
			{
				if (floor.GetRootCount() == 1)
				{
					goalRootFloor.Add(floor);
					playerRoot.Add(floor.GetMapPosition());
				}
			}
			//���Ԃ��Ă���|�W�V�����f�[�^�������������
			for (int i = 0; i < playerRoot.Count; i++)
			{
				if (i != 0 && i != playerRoot.Count)
				{
					if (playerRoot[i].x == playerRoot[i - 1].x && playerRoot[i].z == playerRoot[i - 1].z)
					{
						Debug.Log("���Ԃ���");
						playerRoot.RemoveAt(i);
					}
				}
			}

			//�S�[����position���i�[
			Floor goal = mapObjects.Find(match => match.GetComponent<Floor>().GetFloorState() == "goal").GetComponent<Floor>();
			checkedFloorList.Add(goal);
			playerRoot.Add(goal.GetMapPosition());

			float waitTime = effectCreateTime;

			//�S�[���܂ł̃��[�g�ɃG�t�F�N�g�𐶐�
			foreach (Floor floor in goalRootFloor)
			{
				//Debug.Log(floor.GetRootCount());
				//Debug.Log(floor.GetMapPosition());
				Vector3 pos = floor.transform.position;
				pos.y -= 1.0f;
				StartCoroutine(CreateGoalEffect(waitTime, pos));
				waitTime += 0.1f;
			}

			//�v���C���[���ʂ郋�[�g���i�[
			playerObject.GetComponent<PlayerControl>().SetGoalRoot(playerRoot);
			//�v���C���[������������x��
			WaitPlayerMove(clearwaitTime);

			//���͂���΂悭�ˁH
			onGoal = false;
		}
		//�}�b�v�̃`�F���W���I�t�ɂȂ�����}�b�v��ς����悤�ɂ���
		if (isOff == true)
		{
			if (waitTime >= 30.0f)
			{
				//�}�b�v���N���A���Ă��Ȃ������瑀��ł���
				if (isMapClear == false)
				{
					Debug.Log("�`�F���W�\");
					GetComponent<CursorManager>().enabled = true;
					GetComponent<TouchControl>().enabled = true;
				}
				//�`�F���W���ł���悤�ɂ���
				isOff = false;//DoOnce
				floorChange = false;
			}
			waitTime++;
		}
	}

	public List<GameObject> GetGameObjectList()
	{

		return mapObjects;
	}
	public void AllFloorWaitOff()
	{
		for (int i = 0; i < mapObjects.Count; i++)
		{
			mapObjects[i].GetComponent<Floor>().SetChangeWait(false);
		}
	}
	public void ChangeMap(GameObject obj)//�����������
	{

		checkedFloorList.Clear();
		string state = obj.GetComponent<Floor>().GetFloorState();
		int num = obj.GetComponent<Floor>().GetFaceCount() + 1;

		obj.GetComponent<Floor>().SetFloorState(state);
		obj.GetComponent<Floor>().SetFaceCount(num);
		//�}�b�v�`�F���W�I��
		obj.GetComponent<Floor>().OnChange();

	}

	public void CheckMap()
	{

		GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");

		checkedFloorList.Clear();

		checkedFloorList.Add(player.GetComponent<Floor>());
		player.GetComponent<Floor>().CheckFloor();

	}

	public void LinkChangeFloor(GameObject gameObject)
	{
		GameObject obj_top = null;
		GameObject obj_bottom = null;
		GameObject obj_left = null;
		GameObject obj_right = null;

		//��
		obj_top = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z - 1);
		//��
		obj_bottom = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z + 1);
		//��
		obj_left = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);
		//��
		obj_right = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);


		if (obj_top != null) if (obj_top.GetComponent<Floor>().GetFloorState() == "player") obj_top = null;
		if (obj_bottom != null) if (obj_bottom.GetComponent<Floor>().GetFloorState() == "player") obj_bottom = null;
		if (obj_left != null) if (obj_left.GetComponent<Floor>().GetFloorState() == "player") obj_left = null;
		if (obj_right != null) if (obj_right.GetComponent<Floor>().GetFloorState() == "player") obj_right = null;



		if (obj_top != null)
		{

			obj_top.GetComponent<Floor>().SetFaceCount(obj_top.GetComponent<Floor>().GetFaceCount() + 1);
			obj_top.GetComponent<Floor>().SetFloorState(obj_top.GetComponent<Floor>().GetFloorState());
		}
		if (obj_bottom != null)
		{

			obj_bottom.GetComponent<Floor>().SetFaceCount(obj_bottom.GetComponent<Floor>().GetFaceCount() + 1);
			obj_bottom.GetComponent<Floor>().SetFloorState(obj_bottom.GetComponent<Floor>().GetFloorState());
		}
		if (obj_left != null)
		{

			obj_left.GetComponent<Floor>().SetFaceCount(obj_left.GetComponent<Floor>().GetFaceCount() + 1);
			obj_left.GetComponent<Floor>().SetFloorState(obj_left.GetComponent<Floor>().GetFloorState());
		}
		if (obj_right != null)
		{
			obj_right.GetComponent<Floor>().SetFaceCount(obj_right.GetComponent<Floor>().GetFaceCount() + 1);
			obj_right.GetComponent<Floor>().SetFloorState(obj_right.GetComponent<Floor>().GetFloorState());
		}

	}
	public void LinkChangeFloorMotion(GameObject gameObject)
	{
		GameObject obj_top = null;
		GameObject obj_bottom = null;
		GameObject obj_left = null;
		GameObject obj_right = null;

		//��
		obj_top = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z - 1);
		//��
		obj_bottom = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z + 1);
		//��
		obj_left = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);
		//��
		obj_right = mapObjects.Find(match => match.GetComponent<Floor>().GetMapPosition().x == gameObject.GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == gameObject.GetComponent<Floor>().GetMapPosition().z);


		if (obj_top != null) if (obj_top.GetComponent<Floor>().GetFloorState() == "player") obj_top = null;
		if (obj_bottom != null) if (obj_bottom.GetComponent<Floor>().GetFloorState() == "player") obj_bottom = null;
		if (obj_left != null) if (obj_left.GetComponent<Floor>().GetFloorState() == "player") obj_left = null;
		if (obj_right != null) if (obj_right.GetComponent<Floor>().GetFloorState() == "player") obj_right = null;



		if (obj_top != null)
		{
			obj_top.GetComponent<Floor>().OnLinkChange();
		}
		if (obj_bottom != null)
		{
			obj_bottom.GetComponent<Floor>().OnLinkChange();
		}
		if (obj_left != null)
		{
			obj_left.GetComponent<Floor>().OnLinkChange();
		}
		if (obj_right != null)
		{
			obj_right.GetComponent<Floor>().OnLinkChange();
		}

	}

	public List<Floor> GetCheckedFloorList()
	{
		return checkedFloorList;
	}

	public void IsMapClear()
	{
		isMapClear = true;
	}
	void OnGoal()
	{
		onGoal = true;
	}
	//�S�[�����������x��
	public void WaitOnGoal()
	{
		//���̏����̎d�����ƃ}�b�v�̃T�C�Y���₵����o�O��܂��B
		Invoke(nameof(OnGoal), 1.0f);
	}
	private void PlayerMove()
	{
		Debug.Log("�v���C���[�����܂�");
		playerObject.GetComponent<PlayerControl>().OnPlayerMove();
	}
	public void WaitPlayerMove(float waitTime)
	{

		Invoke(nameof(PlayerMove), waitTime);

	}
	public int GetFaceNum()
	{
		return faceNum;
	}
	public int GetMapSize()
	{
		return textXNumber;
	}
	public void ChangeOff()
	{
		isOff = true;
	}


	//�G�t�F�N�g����
	private IEnumerator CreateGoalEffect(float waitTime, Vector3 pos)
	{
		yield return new WaitForSeconds(waitTime);
		Instantiate(goalEffect, pos, Quaternion.identity);
		pos.y -= 1.0f;
		Instantiate(playerRootEffect, pos, Quaternion.identity);

	}
	//�}�b�v�̃��Z�b�g
	public void MapReset()
	{
		foreach (GameObject obj in mapObjects)
		{
			Destroy(obj);
		}

		mapObjects.RemoveAll(match => match != null);

		int state = 0;

		for (int i = 1; i < textYNumber; i++)
		{
			string[] tempWords = textData[i].Split(',');

			for (int j = 0; j < textXNumber; j++)
			{
				dungeonMap[i, j] = tempWords[j];

				state = int.Parse(dungeonMap[i, j]);

				if (dungeonMap[i, j] != null)
				{
					switch (state)//�X�C�b�`���S�~
					{
						case 0:

							break;
						case 1:
							GameObject floor1 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor1.GetComponent<Floor>().SetParentmap(this);
							floor1.GetComponent<Floor>().SetMapPosition(j, i - 1, "red");
							floor1.transform.Rotate(180.0f, 0.0f, 0.0f);
							floor1.GetComponent<Floor>().SetFaceCount(1);
							mapObjects.Add(floor1);


							break;

						case 2:
							GameObject floor2 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor2.GetComponent<Floor>().SetParentmap(this);
							floor2.GetComponent<Floor>().SetMapPosition(j, i - 1, "blue");
							floor2.transform.Rotate(270.0f, 0.0f, 0.0f);
							floor2.GetComponent<Floor>().SetFaceCount(2);
							mapObjects.Add(floor2);


							break;
						case 3:
							GameObject floor3 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor3.GetComponent<Floor>().SetParentmap(this);
							floor3.GetComponent<Floor>().SetMapPosition(j, i - 1, "yellow");
							floor3.transform.Rotate(0.0f, 0.0f, 0.0f);
							floor3.GetComponent<Floor>().SetFaceCount(3);
							mapObjects.Add(floor3);


							break;
						case 4:
							GameObject floor4 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor4.GetComponent<Floor>().SetParentmap(this);
							floor4.GetComponent<Floor>().SetMapPosition(j, i - 1, "green");
							floor4.transform.Rotate(90.0f, 0.0f, 0.0f);
							mapObjects.Add(floor4);


							break;
						case 5:
							GameObject floor5 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor5.GetComponent<Floor>().SetParentmap(this);
							floor5.GetComponent<Floor>().SetMapPosition(j, i - 1, "goal");
							mapObjects.Add(floor5);


							break;
						case 6:
							GameObject floor6 = Instantiate(Floor, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
							floor6.GetComponent<Floor>().SetParentmap(this);
							floor6.GetComponent<Floor>().SetMapPosition(j, i - 1, "player");
							if (scriptableObject.colorName == "red") floor6.transform.Rotate(180.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "blue") floor6.transform.Rotate(270.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "yellow") floor6.transform.Rotate(0.0f, 0.0f, 0.0f);
							else if (scriptableObject.colorName == "green") floor6.transform.Rotate(90.0f, 0.0f, 0.0f);

							mapObjects.Add(floor6);

							break;
						default:
							break;
					}
				}
			}
		}

	}
}
