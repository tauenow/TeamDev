using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;


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
	private bool check = false;
	private bool mapCheck = false;
	private float mapCheckTime = 0.0f;

	//�S�[���̂��ǂ蒅�����߂̕ϐ�
	private bool onGoal;
	private List<Vector3> playerRoot;

	//�ʂ̐�
	private int faceNum = 0;

	//�S�[���G�t�F�N�g�I�u�W�F�N�g
	[SerializeField]
	private GameObject goalEffect = null;
	[SerializeField]
	private GameObject playerRootEffect = null;

	//�v���C���[���ʂ�F
	[SerializeField]
	private StageScriptableObject scriptableObject;

	private void Start()
	{
		//������
		mapObjects = new();
		checkedFloorList = new();
		floorChange = false;

        playerObject = null;
        check = false;
        mapCheck = false;
		onGoal = false;
        playerRoot = new();

        faceNum = 0;
		//

        string textLines = MapFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��
										 //print(textLines);

		// ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
		textData = textLines.Split('\n');

		// �s���Ɨ񐔂̎擾
		textXNumber = textData[0].Split(',').Length;
		textYNumber = textData.Length;
		textYNumber -= 1;


		// �Q�����z��̒�`
		dungeonMap = new string[textYNumber, textXNumber];//�}�b�v
		int state = 0;

		for (int i = 0; i < 1; i++)
		{
			string[] tempWords = textData[i].Split(',');
			for (int j = 0; j < 1; j++)
			{
				dungeonMap[i, j] = tempWords[j];

				scriptableObject.colorName = dungeonMap[i, j];
			}
		}

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
							if(scriptableObject.colorName == "red") floor6.transform.Rotate(180.0f, 0.0f, 0.0f);
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

		Debug.Log("�Z���^�[��o�^");
		if (centerPosRegister == false)//�Z���^�[�|�X���Ȃ�������o�^����
		{
			startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
			endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

			center = (startPos + endPos) / 2;

			GameObject centerObj = Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity) as GameObject;

			centerPosRegister = true;//��o�^�����OK
			GameObject.Find("Main Camera").GetComponent<CameraControl>().CentrCretae(centerObj);//�J�����̃Z���^�[�ɂȂ�I�u�W�F�N�g��������

		}

		GameObject goalObj = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "goal");

		Vector3 transformLookPos = goalObj.transform.position;
		transformLookPos.y += 1;

		playerObject.transform.LookAt(transformLookPos);

		//�v���C���[���S�[���������悤�ɂ���
	}

	private void Update()
	{

		if (GetComponent<CursorManager>().onGoal == false && GetComponent<TouchControl>().onGoal == false)
		{
			if (onGoal == true)
			{
				List<Floor> goalRootFloor = new();
				parentManager.isClear = true;
				Debug.Log(parentManager.isClear);
				//����Ȃ����[�g������
				for (int i = 0; i < checkedFloorList.Count; i++)
				{
					if (checkedFloorList[i].GetRootCount() == 0)
					{
						checkedFloorList[i].CheckOldRoot();
					}
				}
				//�v���C���[�̃|�W�V��������S�[���̃��[�g�܂ł�root��position���i�[
				for (int i = 0; i < checkedFloorList.Count; i++)
				{
					if (checkedFloorList[i].GetRootCount() != 0)
					{
						playerRoot.Add(checkedFloorList[i].GetMapPosition());
						goalRootFloor.Add(checkedFloorList[i]);
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

				float waitTime = 0.1f;

				//�S�[���܂ł̃��[�g�ɃG�t�F�N�g�𐶐�
				foreach(Floor floor in goalRootFloor)
				{
					Vector3 pos = floor.transform.position;
					pos.y -= 1.0f;
					StartCoroutine(CreateGoalEffect(waitTime, pos));
					waitTime += 0.1f;
				}

				//�S�[�������炢�����悤�ɂ���
				GetComponent<CursorManager>().onGoal = true;
				GetComponent<TouchControl>().onGoal = true;

                //�v���C���[���ʂ郋�[�g���i�[&&�v���C���[���S�[���܂œ����̂�����
                playerObject.GetComponent<PlayerControl>().SetGoalRoot(playerRoot);
				//�v���C���[�������̂�x��
				Invoke(nameof(OnGoal), 2.0f);

                //���͂���΂悭�ˁH
                onGoal = false;
			}



            //�}�b�v�`�F�b�N
            if (check == true)
            {
                mapCheckTime += Time.deltaTime;
            }
            if (mapCheckTime >= 0.5f)
            {
                if (mapCheck == true)
                {
                    CheckMap();
                    mapCheck = false;
                }
            }
            if (mapCheckTime >= 2.0f)
            {
                mapCheckTime = 0.0f;
                check = false;
                floorChange = false;
            }
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

		//���̂����ĕϐ��Ȃ�Ȃ�
		GameObject floor = mapObjects.Find(f => f.gameObject.GetComponent<Floor>() == obj.GetComponent<Floor>());

		checkedFloorList.Clear();
		float x = floor.GetComponent<Floor>().GetMapPosition().x;
		float z = floor.GetComponent<Floor>().GetMapPosition().z;

		floor.GetComponent<Floor>().SetFloorState(floor.GetComponent<Floor>().GetFloorState());

		obj.GetComponent<Floor>().OnChange();//�}�b�v�`�F���W�I��
		obj.GetComponent<Floor>().SetFaceCount(obj.GetComponent<Floor>().GetFaceCount() + 1);

		check = true;
		mapCheck = true;

	}

	public void CheckMap()
	{

		GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");

		checkedFloorList.Clear();

		checkedFloorList.Add(player.GetComponent<Floor>());
		player.GetComponent<Floor>().CheckFloor();

		//�t���A�̏㉺�㉺�̃t���A�ɍs���邩�ǂ���
		//�������ꏊ�ɂ͖߂�Ȃ��悤�ɂ���(�ʂ��Ă����t���A�̍��W��list�ŕۊǂ���Ƃ�)
		//���z�u���Ă���v���C���[���ʂ��t���A��list�����A�Ȃ����Ă��邩�ǂ����̔��������̂͂ǂ��H
		//�z�u���Ă���u���b�N���Ƃɏ�Ɖ��ƍ��ƉE�̃u���b�N�̏����`�F�b�N��������͂ǂ��H ���̗p

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
			obj_top.GetComponent<Floor>().LinkChange();
        }
        if (obj_bottom != null)
        {
            obj_bottom.GetComponent<Floor>().LinkChange();
        }
        if (obj_left != null)
        {
			obj_left.GetComponent<Floor>().LinkChange();
        }
        if (obj_right != null)
        {
            obj_right.GetComponent<Floor>().LinkChange();
        }

    }

    public List<Floor> GetCheckedFloorList()
	{
		return checkedFloorList;
	}
	public void InGoal()
	{
		onGoal = true;
	}
    public void OnGoal()
    {

		playerObject.GetComponent<PlayerControl>().OnPlayerMove();

    }
    public bool GetInGoal()
	{
		return onGoal;
	}

	public int GetFaceNum()
	{
		return faceNum;
	}
	public int GetMapSize()
	{
		return textXNumber;
	}

	//�G�t�F�N�g����
	private IEnumerator CreateGoalEffect(float waitTime, Vector3 pos)
	{
		yield return new WaitForSeconds(waitTime);
		Instantiate(goalEffect,pos,Quaternion.identity);
		pos.y -= 1.0f;
        Instantiate(playerRootEffect, pos, Quaternion.identity);

    }
}
