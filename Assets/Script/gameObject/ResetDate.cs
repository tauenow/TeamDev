using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDate : MonoBehaviour
{
    //���ʃf�[�^
    [SerializeField]
    private StageScriptableObject stageScriptableObject;

    public void OnResetdate()
    {
        //�f�[�^�̏�����
        stageScriptableObject.StageNum = 1;
        stageScriptableObject.DifficultyIndex = 1;
        stageScriptableObject.textIndex = 0;

        stageScriptableObject.tutorialClear = false;
        stageScriptableObject.colorName = "None";
        stageScriptableObject.oldSceneName = "None";
        for(int i = 0;i < stageScriptableObject.isClearList.Count;i++)
        {
            stageScriptableObject.isClearList[i] = false;
        }
    }

}
