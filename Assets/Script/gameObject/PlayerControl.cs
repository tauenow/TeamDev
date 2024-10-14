using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //�v���C���[���������̑���

    [Header("�v���C���[�̓������̑���")]
    [SerializeField]
    private float playerMoveTime = 0.0f;
    private float moveTime = 0.0f;//�������������
    private int moveCount = 1;//�؂�ւ���
    private float currentTime = 0.0f;//�o�ߎ���
    [Header("���炩�ɓ����x��")]
    [SerializeField]
    private int moveFrame = 10;
    private float currentFrame = 0.0f;
    //�v���C���[�̃S�[���܂ł̃��[�g���i�[����list
    private List<Vector3> goalRoot = new();
    private bool onGoal = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(onGoal == true)
        {
            moveTime = (playerMoveTime * goalRoot.Count) / goalRoot.Count;
            //�b���X�V
            currentTime += Time.deltaTime;

            if (moveCount - 1 >= goalRoot.Count)//�}�W�b�N�i���o�[���߂�
            {
                onGoal = false;
            }
            else if (currentTime <= moveTime * moveCount)
            { 
                //�E���ɍs������
                if (transform.position.x < goalRoot[moveCount - 1].x)
                {
                    if (currentTime >= (moveTime * moveCount) * (currentFrame / moveFrame))
                    {
                        Vector3 transformPos = transform.position;
                        transformPos.x -= (1.0f / moveFrame);
                        transform.position = transformPos;
                        currentFrame++;
                    }
                    else if (currentFrame >= moveFrame)
                    {
                        Debug.Log("������؂�ւ��܂�");
                        currentFrame = 0;
                    }
                }
                

            }
            else if (currentTime > moveTime * moveCount)
            {
                moveCount++;
            }

        }

    }

    public void SetGoalRoot(List<Vector3> root)
    {
        goalRoot = root;
    }
    public void OnPlayerMove()
    {
        onGoal = true;
    }
}
