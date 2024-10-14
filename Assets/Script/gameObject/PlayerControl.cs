using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //プレイヤーが動く時の速さ

    [Header("プレイヤーの動く時の速さ")]
    [SerializeField]
    private float playerMoveTime = 0.0f;
    private float moveTime = 0.0f;//一つ動く総合時間
    private int moveCount = 1;//切り替え回数
    private float currentTime = 0.0f;//経過時間
    [Header("滑らかに動く度合")]
    [SerializeField]
    private int moveFrame = 10;
    private float currentFrame = 0.0f;
    //プレイヤーのゴールまでのルートを格納するlist
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
            //秒数更新
            currentTime += Time.deltaTime;

            if (moveCount - 1 >= goalRoot.Count)//マジックナンバーごめん
            {
                onGoal = false;
            }
            else if (currentTime <= moveTime * moveCount)
            { 
                //右側に行く処理
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
                        Debug.Log("動きを切り替えます");
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
