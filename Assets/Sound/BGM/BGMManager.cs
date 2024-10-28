using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [SerializeField] private StageScriptableObject scriptableObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource titleBGM;
    public AudioSource stageSelectBGM;
    public AudioSource easyBGM;
    public AudioSource mediumBGM;
    public AudioSource hardBGM;

    private AudioSource currentBGM;
    private string previousScene;

    private void Start()
    {
        previousScene = SceneManager.GetActiveScene().name;
        PlayBGMForScene(previousScene);

        // �V�[���ύX���̃C�x���g��o�^
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        PlayBGMForScene(nextScene.name);
    }

    private void PlayBGMForScene(string sceneName)
    {
        // ������BGM���~
        if (currentBGM != null)
        {
            currentBGM.Stop();
        }

        // �V�[���Ɋ�Â�BGM�؂�ւ�
        if (sceneName == "TitleScene")
        {
            currentBGM = titleBGM;
        }
        else if (sceneName == "StageSelect")
        {
            currentBGM = stageSelectBGM;
        }
        else if (sceneName == "SampleScene")
        {
            // ��Փx�Ɋ�Â�BGM�I��
            switch (scriptableObject.DifficultyIndex)
            {
                case 1:
                    currentBGM = easyBGM;
                    break;
                case 2:
                    currentBGM = mediumBGM;
                    break;
                case 3:
                    currentBGM = hardBGM;
                    break;
                default:
                    Debug.LogWarning("�����ȓ�Փx�C���f�b�N�X");
                    return;
            }
        }

        // �I������BGM���Đ�
        if (currentBGM != null)
        {
            currentBGM.Play();
        }

        // ���݂̃V�[������ۑ�
        previousScene = sceneName;
    }
}
