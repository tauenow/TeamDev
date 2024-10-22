using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    //�X�N���v�^�Ԃ�I�u�W�F�N�g��Clear��-1����
    //�X�e�[�W�Z���N�g���牽�Ԗڂ̃X�e�[�W��I�񂾂�������ϐ�
    [SerializeField] private StageScriptableObject StageObj;
    private int selecetStageNum = 1;
    [SerializeField] private GameObject map;
    //�}�b�v�t�@�C���ꗗ
    [SerializeField]
    private TextAsset MapFile1;
    List<TextAsset> mapFaileList = new();

    [Header("�t�F�[�h")]
    [SerializeField] private Image fade = default;

    private GameObject mapObject;
    //���̃X�e�[�W���N���A�������ǂ���
    public bool isClear = false;

    void Start()
    {
        //���X�g����Ăяo���Ƃ���-1���Ă�
        //�e�N�X�g�t�@�C���̃��X�g�Ƀ}�b�v�̃f�[�^��S�Ċi�[����
        mapFaileList.Add(MapFile1);

        if(selecetStageNum == 1)
        {
            mapObject = Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            mapObject.GetComponent<MapManager>().MapFile = MapFile1;
            mapObject.GetComponent<MapManager>().parentManager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoneGoal()
    {

    }
    public GameObject GetMapObject()
    {
        return mapObject;
    }
    public void ChangeScene()
    {
        StageObj.isClearList[selecetStageNum - 1] = true;
        fade.GetComponent<FadeINOUT>().FadeToChangeScene();
    }
}
