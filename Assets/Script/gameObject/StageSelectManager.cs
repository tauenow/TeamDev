using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update

    //�X�e�[�W�Z���N�g���牽�Ԗڂ̃X�e�[�W��I�񂾂�������ϐ�
    [SerializeField] private StageScriptableObject StageObj;
    private int selecetStageNum = 1;
    [SerializeField] private MapManager map;
    //�}�b�v�t�@�C���ꗗ
    [SerializeField]
    private TextAsset MapFile1;

    //���̃X�e�[�W���N���A�������ǂ���
    private bool isClear = false;

    void Start()
    {
        StageObj.isClearList.Add(false);
        //selecetStageNum = StageObj.StageNum;

        //������ʎY����΂ł����
        if(selecetStageNum == 1)
        {
            Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity).MapFile = MapFile1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoneGoal()
    {

    }
}
