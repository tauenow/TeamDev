using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �{�^�����g�p���邽��UI��SceneManage�ǉ�
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // �V�[���؂�ւ�
    public void ChangeSceneToStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void ChangeSceneToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
