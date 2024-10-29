using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// �X���㓡��̃v���C���[�̐F��ύX����X�N���v�g�B
/// �R�[�f�B���O�Ƃ������������z�Ȃ�ŁA������������E���Ă��������B
/// </summary>
public class PlayerColor : MonoBehaviour
{
    [Header("���L�I�u�W�F�N�g")][SerializeField] private StageScriptableObject scriptableObject;

    [SerializeField] Texture[] playerColor = new Texture[4];

    //�q�I�u�W�F�N�g���i�[����ϐ�
    public GameObject cup;
    public GameObject sotai;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̎q�I�u�W�F�N�g���擾
        cup = GameObject.Find("cup");
        sotai = GameObject.Find("sotai");

        //stage�X�N���v�g�I�u�W�F�N�g����Q�ƁB
        switch (scriptableObject.colorName)
        {
            //Material��Texture�����������ăv���C���[�̃J���[�ύX
            //�ԂȂ�z��0�Ԗڂ�
            case "red":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[0];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[0];
                break;
            //�Ȃ�z��1�Ԗڂ�
            case "blue":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[1];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[1];
                break;
            //���F�Ȃ�z��2�Ԗڂ�
            case "yellow":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[2];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[2];
                break;
            //�΂Ȃ�z��3�Ԗڂ�
            case "green":
                cup.GetComponent<Renderer>().material.mainTexture = playerColor[3];
                sotai.GetComponent<Renderer>().material.mainTexture = playerColor[3];
                break;
            //��O�p
            default:
                cup.GetComponent<Renderer>().material.mainTexture = null;
                sotai.GetComponent<Renderer>().material.mainTexture = null;
                Debug.Log("�v���C���[�F�G���[");
                break;
        }
    }
}
