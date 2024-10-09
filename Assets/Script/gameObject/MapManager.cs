using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapManager : MonoBehaviour
{
	[SerializeField] private StageScriptableObject StageNum;
	private int Stage;

	[Header("�}�b�v�̃f�[�^�t�@�C��")]
	[SerializeField]
	private TextAsset MapFile;

	private string[] textData;
	private string[,] dungeonMap;

	private int textXNumber; // �s���ɑ���
	private int textYNumber; // �񐔂ɑ���

	bool centerPosRegister = false;//�}�b�v�̃Z���^�[�|�W�V���������邩�Ȃ���

	[Header("�Ԃ��u���b�N")]
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

	//�Z���^�[�̃|�W�V�������߂邽�߂̕ϐ��B
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 center;

	//Floor��list
	List<GameObject> mapObjects = new();
	//�`�F�b�N����Ƃ��̒ʂ����I�u�W�F�N�g���i�[����
	List<Floor> oldlist = new();

	//�o�����u���b�N�̍ŏ����W�ƍő���W���c���Ƃ�
	private float minPosX = 0.0f;
	private float minPosZ = 0.0f;
	private float maxPosX = 0.0f;
	private float maxPosZ = 0.0f;

	private void Start()
	{
		Stage = StageNum.StageNum;
		Debug.Log(Stage);

		string textLines = MapFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��
		print(textLines);

		// ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
		textData = textLines.Split('\n');

		// �s���Ɨ񐔂̎擾
		textXNumber = textData[0].Split(',').Length;
		textYNumber = textData.Length;

		textYNumber -= 1;


		// �Q�����z��̒�`
		dungeonMap = new string[textYNumber, textXNumber];//�}�b�v

		//Debug.Log("�}�b�v");
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
					Debug.Log("�łĂ܂�" + state);
				}

				if (dungeonMap[i, j] != null)
				{
					switch (state)//�X�C�b�`���S�~
					{
						case 0:

							break;
						case 1:
							if (j == 6 && i == 0)
							{
								Debug.Log("�łĂ܂�" + dungeonMap[i, j]);
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

							Debug.Log("�v���C���[����");
							//�v���C���[����
							Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity);

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

			Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);

			centerPosRegister = true;//��o�^�����OK
		}


		//�}�b�v�̈�ԍŏ��̃I�u�W�F�N�g�̍��W�ƈ�ԍŌ�̃I�u�W�F�N�g�̍��W���i�[
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
	public void ChangeMap(GameObject obj)//�����������
	{
		//���̂����ĕϐ��Ȃ�Ȃ�
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

		//�t���A�̏㉺�㉺�̃t���A�ɍs���邩�ǂ���
		//�������ꏊ�ɂ͖߂�Ȃ��悤�ɂ���(�ʂ��Ă����t���A�̍��W��list�ŕۊǂ���Ƃ�)
		//���z�u���Ă���v���C���[���ʂ��t���A��list�����A�Ȃ����Ă��邩�ǂ����̔��������̂͂ǂ��H
		//�z�u���Ă���u���b�N���Ƃɏ�Ɖ��ƍ��ƉE�̃u���b�N�̏����`�F�b�N��������͂ǂ��H ���̗p



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
