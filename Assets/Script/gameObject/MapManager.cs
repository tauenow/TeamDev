using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;



public class MapManager : MonoBehaviour
{


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

    //�v���C���[��ݒ�
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

    //�o�����u���b�N�̍ŏ����W�ƍő���W���c���Ƃ�
    private float minPosX = 0.0f;
    private float minPosZ = 0.0f;
    private float maxPosX = 0.0f;
    private float maxPosZ = 0.0f;

    //�}�b�v�`�F�b�NTime
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
                            if (j == 6 && i == 0)
                            {
                                Debug.Log("�łĂ܂�" + dungeonMap[i, j]);
                            }

                            GameObject floor1 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor1.GetComponent<Floor>().SetMapPosition(j, i, "red");
                            floor1.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor1);
                            floor1.GetComponent<Floor>().SetParentmap(this);

                            if(faceNum  < 1)
                            {
                                faceNum = 1;
                            }

                            break;

                        case 2:

                            GameObject floor2 = Instantiate(blueFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor2.GetComponent<Floor>().SetMapPosition(j, i, "blue");
                            floor2.transform.Rotate(90.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor2);
                            floor2.GetComponent<Floor>().SetParentmap(this);

                            if (faceNum < 2)
                            {
                                faceNum = 2;
                            }

                            break;
                        case 3:

                            GameObject floor3 = Instantiate(Goal, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor3.GetComponent<Floor>().SetMapPosition(j, i, "goal");
                            mapObjects.Add(floor3);
                            floor3.GetComponent<Floor>().SetParentmap(this);

                            break;
                        case 4:

                            GameObject floor4 = Instantiate(redFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor4.GetComponent<Floor>().SetMapPosition(j, i, "player");
                            floor4.transform.Rotate(180.0f, 0.0f, 0.0f);
                            mapObjects.Add(floor4);
                            floor4.GetComponent<Floor>().SetParentmap(this);

                            //�v���C���[����
                            playerObject = Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity) as GameObject;
                            playerObject.GetComponent<PlayerControl>().SetMapPosition(floor4.GetComponent<Floor>().GetMapPosition());

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

        if(mapCheck == true)
        {
            mapCheckTime += Time.deltaTime;
        }
        if(mapCheckTime >= 6.0f)
        {
            CheckMap();
            CursorManager.floorChange = false;
        }
       


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


            Debug.Log("�S�[��");
            for(int i = 0;i <playerRoot.Count;i++)
            {

                Debug.Log(playerRoot[i].x);
                Debug.Log(playerRoot[i].z);

            }
            

            //�v���C���[���ʂ郋�[�g���i�[&&�v���C���[���S�[���܂œ����̂�����
            playerObject.GetComponent<PlayerControl>().SetGoalRoot(playerRoot);
            playerObject.GetComponent<PlayerControl>().OnPlayerMove();
            //���͂���΂悭�ˁH
            onGoal = false;
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

        mapCheck = true;

    }

    public void CheckMap()
    {

        GameObject player = mapObjects.Find(match => match.gameObject.GetComponent<Floor>().GetFloorState() == "player");
        
        oldlist.Clear();

        oldlist.Add(player.GetComponent<Floor>());
        player.GetComponent<Floor>().CheckFloor();

        mapCheck = false;
        mapCheckTime = 0.0f;
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
    public void InGoal()
    {
        onGoal = true;
    }

    public int GetFaceNum()
    {
        return faceNum;
    }

}
