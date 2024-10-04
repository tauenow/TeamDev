using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private bool cursor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cursor == true)
        {
            Vector3 pos = transform.position;
            pos.y = 1.0f;//マジックナンバーごめん
            transform.position = pos;
            cursor = false;
            return;
        }
        else if(cursor == false)
        {
            Vector3 pos = transform.position;
            pos.y = 0.0f;//マジックナンバーごめん
            transform.position = pos;
        }
    }

    void OnCursor()
    {
        cursor = true;
    }
}
