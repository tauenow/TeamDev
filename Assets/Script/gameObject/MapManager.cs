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

    [Header("�}�b�v�̃f�[�^�t�@�C��")]
    public TextAsset MapFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int textXNumber; // �s���ɑ���
    private int textYNumber; // �񐔂ɑ���

    bool centerPosRegister = false;//�}�b�v�̃Z���^�[�|�W�V���������邩�Ȃ���

    [Header("�u���b�N")]
    [SerializeField]
    private GameObject FloorPrefab_2color;
    [SerializeField]
    private GameObject FloorPrefab_3color;
    [SerializeField]
    private GameObject FloorPrefab_4color;

    //�����悤
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
    private List<GameObject> mapObjects = new();
    //�`�F�b�N����Ƃ��̒ʂ����I�u�W�F�N�g���i�[����
    private List<Floor> oldlist = new();

    //�}�b�v�`�F�b�NTime
    private bool check = false;
    private bool mapCheck = false;
    private float mapCheckTime = 0.0f;

    //�S�[���̂��ǂ蒅�����߂̕ϐ�
    private bool onGoal;
    private List<Vector3> playerRoot = new();

    private float currentTime = 0.0f;

    //�ʂ̐�
    private int faceNum = 0;

    private void Start()
    {
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

        //�ʂ̐��̎w��
        for (int i = 0; i < textYNumber; i++)
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
            Debug.Log("�e�N�X�`����ύX");
        }
        if (faceNum == 3)
        {
            Floor = FloorPrefab_3color;
            Debug.Log("�e�N�X�`����ύX");
        }
        if (faceNum == 4)
        {
            Floor = FloorPrefab_4color;
            Debug.Log("�e�N�X�`����ύX");
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
                    switch (state)//�X�C�b�`���S�~
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

                            //�v���C���[����
                            playerObject = Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity) as GameObject;
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

            Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);

            centerPosRegister = true;//��o�^�����OK
        }

    }

    private void Update()
    {

        if(onGoal == true)
        {
            //����Ȃ����[�g������
            for(int i = 0;i < oldlist.Count;i++)
            {
                if (oldlist[i].GetRootCount() == 0)
                {
                    oldlist[i].CheckOldRoot();
                }
            }
            //�v���C���[�̃|�W�V��������S�[���̃��[�g�܂ł�root��position���i�[
            for (int i = 0; i < oldlist.Count; i++)
            {
                if (oldlist[i].GetRootCount() != 0)
                {
                    playerRoot.Add(oldlist[i].GetMapPosition());
                }
            }
            //�S�[����position���i�[
            Floor goal = oldlist.Find(match => match.GetFloorState() == "goal");
            playerRoot.Add(goal.GetMapPosition());

            //�v���C���[�̃|�W�V�����ɋ߂����Ƀ\�[�g
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

            //�v���C���[���ʂ郋�[�g���i�[&&�v���C���[���S�[���܂œ����̂�����
            playerObject.GetComponent<PlayerControl>().SetGoalRoot(playerRoot);
            playerObject.GetComponent<PlayerControl>().OnPlayerMove();
            //�S�[�������炢�����悤�ɂ���
            GetComponent<CursorManager>().onGoal = true;
            //���͂���΂悭�ˁH
            
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
    public void ChangeMap(GameObject obj)//�����������
    {
        
        //���̂����ĕϐ��Ȃ�Ȃ�
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

        //�t���A�̏㉺�㉺�̃t���A�ɍs���邩�ǂ���
        //�������ꏊ�ɂ͖߂�Ȃ��悤�ɂ���(�ʂ��Ă����t���A�̍��W��list�ŕۊǂ���Ƃ�)
        //���z�u���Ă���v���C���[���ʂ��t���A��list�����A�Ȃ����Ă��邩�ǂ����̔��������̂͂ǂ��H
        //�z�u���Ă���u���b�N���Ƃɏ�Ɖ��ƍ��ƉE�̃u���b�N�̏����`�F�b�N��������͂ǂ��H ���̗p

    }

    public void linkChangeFloor(GameObject gameObject)
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
