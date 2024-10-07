using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{


    [Header("�}�b�v�̃f�[�^�t�@�C��")]
    [SerializeField]
    private TextAsset textFile;

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
    private void Start()
    {
        string textLines = MapFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��
        //print(textLines);

        // ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
        textData = textLines.Split('\n');

        // �s���Ɨ񐔂̎擾
        textXNumber = textData[0].Split(',').Length;
        textYNumber = textData.Length;

        // �Q�����z��̒�`
        dungeonMap = new string[textYNumber, textXNumber];//�}�b�v
        print(dungeonMap);
        Debug.Log("�}�b�v");
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

                            //�v���C���[����
                            Instantiate(Player, new Vector3(transform.position.x + j, transform.position.y + 1.0f, transform.position.z - i), Quaternion.identity);


                            break;

                    }
                    if (centerPosRegister == false)//�Z���^�[�|�X���Ȃ�������o�^����
                    {
                        startPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
                        endPos = new Vector3(transform.position.x + (textXNumber - 1), 0.0f, transform.position.z - (textYNumber - 1));

                        center = (startPos + endPos) / 2;

                        Instantiate(centerObject, new Vector3(center.x, transform.position.y, center.z), Quaternion.identity);


                        centerPosRegister = true;//��o�^�����OK
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
    public void ChangeMap(GameObject obj)//�����������
    {
        //���̂����ĕϐ��Ȃ�Ȃ�
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

        //�t���A�̏㉺�㉺�̃t���A�ɍs���邩�ǂ���
        //�������ꏊ�ɂ͖߂�Ȃ��悤�ɂ���(�ʂ��Ă����t���A�̍��W��list�ŕۊǂ���Ƃ�)
        //���z�u���Ă���v���C���[���ʂ��t���A��list�����A�Ȃ����Ă��邩�ǂ����̔��������̂͂ǂ��H
        //�z�u���Ă���u���b�N���Ƃɏ�Ɖ��ƍ��ƉE�̃u���b�N�̏����`�F�b�N��������͂ǂ��H









    }

}
