using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class MapLoad : MonoBehaviour
{

    [Header("�}�b�v�̃f�[�^�t�@�C��")]
    [SerializeField]
    private TextAsset textFile;

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
    private GameObject centerObject;
    [SerializeField]
    private GameObject GameManager;

    //�Z���^�[�̃|�W�V�������߂邽�߂̕ϐ��B
    private Vector3 startPos;  
    private Vector3 endPos;
    private Vector3 center;

    //Floor��list
    List<GameObject> floors = new();
    private void Start()
    {
        string textLines = textFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��
        //print(textLines);

        // ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
        textData = textLines.Split('\n');

        // �s���Ɨ񐔂̎擾
        textXNumber = textData[0].Split(',').Length;
        textYNumber = textData.Length;

        // �Q�����z��̒�`
        dungeonMap = new string[textYNumber, textXNumber];//�}�b�v
        
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
                            floor1.GetComponent<Floor>().SetMapPosition(i, j, "1");
                            floors.Add(floor1);
                            Debug.Log(i);
                            Debug.Log(j);
                            break;

                        case "2":

                            GameObject floor2 = Instantiate(blueFloorPrefab, new Vector3(transform.position.x + j, transform.position.y, transform.position.z - i), Quaternion.identity) as GameObject;
                            floor2.GetComponent<Floor>().SetMapPosition(i, j, "2");
                            floors.Add(floor2);


                            break;
                            
                    }
                    if(centerPosRegister == false)//�Z���^�[�|�X���Ȃ�������o�^����
                    {
                        startPos = new  Vector3(transform.position.x,0.0f, transform.position.z);
                        endPos = new Vector3(transform.position.x + (textXNumber - 1),0.0f, transform.position.z - (textYNumber - 1));

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

    public void ChangeMap(GameObject obj)//�����������
    {
        //���̂����ĕϐ��Ȃ�Ȃ�
        GameObject floor = floors.Find(f => f.gameObject.GetComponent<Floor>()== obj.GetComponent<Floor>());
        

        float x = floor.GetComponent<Floor>().GetMapPosition().x;
        float z = floor.GetComponent<Floor>().GetMapPosition().z;

        for (int i = 0; i < textYNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');

            for (int j = 0; j < textXNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];

                if (dungeonMap[i, j] != null)
                {
                   if(i == x&& j == z)
                    {
                        floor.GetComponent<Floor>().SetFloorState(floor.GetComponent<Floor>().GetFloorState());
                        Debug.Log(i);
                        Debug.Log(j);
                    }
                }
            }
        }

    }

    
}
