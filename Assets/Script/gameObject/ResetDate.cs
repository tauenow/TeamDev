using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDate : MonoBehaviour
{
    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject stageScriptableObject;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResetdate()
    {
        //�f�[�^�̏�����
        stageScriptableObject.StageNum = 1;
        stageScriptableObject.DifficultyIndex = 1;
        stageScriptableObject.textIndex = 0;

        stageScriptableObject.tutorialClear = false;
        for(int i = 0;i < stageScriptableObject.isClearList.Count;i++)
        {
            stageScriptableObject.isClearList[i] = false;
        }
    }

}
