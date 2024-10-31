using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour
{
    
    private GameObject thisGameObject = null;
	//�}�E�X�̏�����OnOff
	[SerializeField]
	private bool Mouse = false;
	//�^�b�`�����OnOff
    [SerializeField]
    private bool Tap = false;

	//Floor�̕ϐ�
	private Vector3 position;//����Floor��2�����z��̃|�W�V����
	private string state;//�Q�����z��œo�^����Ă��镶���i���o�[
	private MapManager parentMap = null;
	private Floor oldFloor = null;//����Floor�̃`�F�b�N�ɓ���O�̏��
	private int rootCount = 0;//����Floor����̕���̐�

	//ChangeFloor�̕ϐ�
	[SerializeField]
	private float motionFrame = 0.1f;
	private int motionCount = 0;
	private float currentTime = 0.0f;
    [Header("�`�F���W���[�V�����̑���")]
    //���[�V�����̑���
    [SerializeField]
	private int changeMotionCount = 10;

	private bool changeWait = false;
	private bool change = false; //�����ς��
	private float currentLinkTime = 0.0f;
	private int  motionLinkCount = 0;
	private bool linkChange = false;

	[SerializeField]
	private float Hight;
	//�J�[�\�����������Ă��邩
	private bool cursor = false;
	//�F�̐�
	private int  faceCount = 1;
	//�F����ς���
	private bool doOnec = true;

	//�I��ł�Ƃ��͐F�𔭍s������
	[SerializeField]
	private float blockPlayerEmissive = 0.0f;
	[SerializeField]
	private float blockEmissive = 0.0f;

	[Header("���L�I�u�W�F�N�g")][SerializeField] private StageScriptableObject scriptableObject;

	[Header("�G�t�F�N�g")]
	[SerializeField]
	private GameObject effectObject = null;
	private GameObject effect = null;
	[SerializeField]
	private GameObject linkEffectObject = null;

	// Start is called before the first frame update
	void Start()
	{
		//������
        oldFloor = null;
		rootCount = 0;
        motionCount = 0;
        currentTime = 0.0f;
        changeWait = false;
        change = false; //�����ς��
        currentLinkTime = 0.0f;
        motionLinkCount = 0;
        linkChange = false;

        cursor = false;
        doOnec = true;

        thisGameObject = parentMap.GetGameObjectList().Find(match => match == gameObject);
    }

	// Update is called once per frame
	void Update()
	{

		
		if (Tap == true) TouchUpdate();
		if (Mouse == true) CursorUpdate();

        ChangeFloor();
        LinkChangeFloor();

	}

	
	public Vector3 GetMapPosition()
	{
		return position;
	}

	public string GetFloorState()
	{
		return state;
	}

	public void SetParentmap(MapManager map)
	{
		parentMap = map;
	}
	public int GetRootCount()
	{
		return rootCount;
	}

	public void SetOldFloor(Floor floor)
	{
		oldFloor = floor;
	}
	public void SetFaceCount(int count)
	{
		faceCount = count;
	}
	public int GetFaceCount()
	{
		return faceCount;
	}
    public void OnChange()
    {
        change = true;
        currentTime = 0.0f;

    }
    public void OnLinkChange()
    {
        linkChange = true;
        currentLinkTime = 0.0f;
    }
    public void OnCursor()
    {
        cursor = true;
    }

    public bool GetChangeState()
    {
        return change;
    }
    public bool GetLinkChangeState()
    {
        return linkChange;
    }
    public void SetChangeWait(bool value)
    {
        changeWait = value;
    }
    public bool GetChangeWait()
    {
        return changeWait;
    }
    public void SetMapPosition(float posX, float posZ, string num)
    {

        position.x = posX;
        position.z = posZ;
        state = num;

        //���x�ύX
        if (state == "player") GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
        else if (state == "goal") GetComponent<MeshRenderer>().material.color = Color.white;
        else if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
        else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;

    }
    public void SetFloorState(string num)
    {

        if (parentMap.GetFaceNum() == 2)//��F�̏ꍇ
        {
            if (num == "red")
            {
                state = "blue";
            }
            else if (num == "blue")
            {
                state = "red";
            }
        }
        if (parentMap.GetFaceNum() == 3)//�O�F�̏ꍇ
        {
            if (num == "red")
            {
                state = "blue";
            }
            else if (num == "blue")
            {
                state = "yellow";
            }
            else if (num == "yellow")
            {
                state = "red";
            }
        }
        if (parentMap.GetFaceNum() == 4)//�l�F�̏ꍇ
        {
            if (num == "red")
            {
                state = "blue";
            }
            else if (num == "blue")
            {
                state = "yellow";
            }
            else if (num == "yellow")
            {
                state = "green";
            }
            else if (num == "green")
            {
                state = "red";
            }
        }

    }

    private void ChangeFloor()
    {
        if (change == true)//���̐F��ς��郂�[�V����
        {
            //��񂵂�����Ȃ��悤�ɂ���
            if (doOnec == true)
            {
                //���ӂ̐F����ύX
                parentMap.LinkChangeFloor(thisGameObject);
                //�v���C���[���I�������ʒu�Ɍ���
                Vector3 lookPos = transform.position;
                lookPos.y += 0.1f;
                GameObject.Find("Player(Clone)").transform.LookAt(lookPos);

                doOnec = false;
            }
            currentTime += Time.deltaTime;

            if (motionCount < changeMotionCount * 3)
            {
                if (currentTime >= motionFrame)
                {
                    //2�ʂ�4�ʂ̎��͒ʏ�
                    if (parentMap.GetFaceNum() == 2 || parentMap.GetFaceNum() == 4)
                    {
                        if (motionCount < changeMotionCount)
                        {
                            SEManager.Instance.PlaySE("ColorChange");
                            Vector3 transformPos = transform.position;
                            transformPos.y += 0.1f;
                            transform.position = transformPos;
                            //transform.Rotate((90.0f * 1.0f / changeMotionCount) / 2, 0.0f, 0.0f);
                        }
                        else if (motionCount < changeMotionCount * 2)
                        {
                            SEManager.Instance.PlaySE("ColorChange");
                            transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);

                        }
                        else if (motionCount < changeMotionCount * 3)
                        {
                            Vector3 transformPos = transform.position;
                            transformPos.y -= 0.1f;
                            transform.position = transformPos;
                            //transform.Rotate((90.0f * 1.0f / changeMotionCount) / 2, 0.0f, 0.0f);
                        }
                        motionCount++;
                        currentTime = 0.0f;
                    }
                    //3�ʂ̎�
                    else if (parentMap.GetFaceNum() == 3)
                    {
                        Debug.Log("3�F");
                        if (motionCount < changeMotionCount)
                        {
                            Vector3 transformPos = transform.position;
                            transformPos.y += 0.1f;
                            transform.position = transformPos;

                            //if (faceCount >= 4)
                            //{
                            //    Debug.Log("z��");
                            //    SEManager.Instance.PlaySE("ColorChange");
                            //    transform.Rotate(0.0f, 0.0f, (90.0f * 1.0f / changeMotionCount) / 2);
                            //}
                            //else if (faceCount >= 1)
                            //{
                            //    Debug.Log("x��");
                            //    SEManager.Instance.PlaySE("ColorChange");
                            //    transform.Rotate((90.0f * 1.0f / changeMotionCount) / 2, 0.0f, 0.0f);
                            //}

                        }
                        else if (motionCount < changeMotionCount * 2)
                        {
                            if (faceCount >= 4)
                            {
                                Debug.Log("z��");
                                SEManager.Instance.PlaySE("ColorChange");
                                transform.Rotate(0.0f, 0.0f, 90.0f * 1.0f / changeMotionCount);
                            }
                            else if (faceCount >= 1)
                            {
                                Debug.Log("x��");
                                SEManager.Instance.PlaySE("ColorChange");
                                transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
                            }

                        }
                        else if (motionCount < changeMotionCount * 3)
                        {
                            Vector3 transformPos = transform.position;
                            transformPos.y -= 0.1f;
                            transform.position = transformPos;
                            //if (faceCount >= 4)
                            //{
                            //    Debug.Log("z��");
                            //    SEManager.Instance.PlaySE("ColorChange");
                            //    transform.Rotate(0.0f, 0.0f, (90.0f * 1.0f / changeMotionCount) / 2);
                            //}
                            //else if (faceCount >= 1)
                            //{
                            //    Debug.Log("x��");
                            //    SEManager.Instance.PlaySE("ColorChange");
                            //    transform.Rotate((90.0f * 1.0f / changeMotionCount) / 2, 0.0f, 0.0f);
                            //}
                        }
                        motionCount++;
                        currentTime = 0.0f;
                    }

                }
            }
        }
        if (motionCount >= changeMotionCount * 3)
        {
            //���[�V�������I�������}�b�v���`�F�b�N
            parentMap.CheckMap();
            //�����N����Floor�̃��[�V��������
            parentMap.LinkChangeFloorMotion(gameObject);
            parentMap.AllFloorWaitOff();

            if (parentMap.GetFaceNum() == 3)
            {
                if (faceCount >= 4)
                {
                    transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
                    faceCount = 1;
                }
            }
            //���x�ύX
            //if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
            //else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;

            //�u���b�N���͂܂������̃G�t�F�N�g�𐶐�
            effect = Instantiate(effectObject, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
            effect.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            //�u���b�N���͂܂������̃G�t�F�N�g��������Ƀ����N���Ă���G�t�F�N�g�𐶐�
            Invoke(nameof(CreateLinkEffect), 0.2f);

            motionCount = 0;
            currentTime = 0.0f;

            changeWait = false;
            change = false;
            doOnec = true;
            SEManager.Instance.PlaySE("Block_Fit");
        }
    }
    //�}�E�X�ł̑���
    private void CursorUpdate()
    {
        if (change == false)//�J�[�\�����������Ă��鎞�̃��[�V����
        {
            if (linkChange == false)
            {
                if (cursor == true)
                {
                    Vector3 pos = transform.position;
                    pos.y = 0.3f;//�}�W�b�N�i���o�[���߂�
                    transform.position = pos;
                    cursor = false;
                    return;
                }
                else if (cursor == false)
                {
                    Vector3 pos = transform.position;
                    pos.y = 0.0f;//�}�W�b�N�i���o�[���߂�
                    transform.position = pos;

                }
            }
        }

    }
    //�X�}�z�^�b�v�ł̑���
    private void TouchUpdate()
    {
        if (change == false)//�J�[�\�����������Ă��鎞�̃��[�V����
        {
            if (linkChange == false)
            {
                if (changeWait == true)
                {
                    Vector3 pos = transform.position;
                    pos.y = 0.3f;//�}�W�b�N�i���o�[���߂�
                    transform.position = pos;
                    return;
                }
                else if (changeWait == false)
                {
                    Vector3 pos = transform.position;
                    pos.y = 0.0f;//�}�W�b�N�i���o�[���߂�
                    transform.position = pos;

                }
            }
        }
    }
    private void LinkChangeFloor()
    {
        //����̃I�u�W�F�N�g�������郂�[�V����
        if (linkChange == true)
        {
            currentLinkTime += Time.deltaTime;

            if (currentLinkTime > motionFrame)
            {
                //2�ʂ�4�ʂ̎��͒ʏ�
                if (parentMap.GetFaceNum() == 2 || parentMap.GetFaceNum() == 4)
                {
                    if (motionLinkCount >= changeMotionCount)
                    {

                        //Debug.Log("���[�V�����I��");
                        //����̕ύX���I������珉����
                        currentLinkTime = 0.0f;
                        motionLinkCount = 0;

                        linkChange = false;
                        //�}�b�v�̃`�F���W���I��������Ƃ�MapManager�ɓ`����
                        parentMap.GetComponent<MapManager>().ChangeOff();
                    }
                    else if (motionLinkCount < changeMotionCount)
                    {
                        transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
                        currentLinkTime = 0.0f;
                        //���x�ύX
                        //if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
                        //else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;
                        motionLinkCount++;
                    }
                }
                //3�ʂ̎�
                else if (parentMap.GetFaceNum() == 3)
                {
                    if (motionLinkCount >= changeMotionCount)
                    {
                        //����̕ύX���I������珉����
                        currentLinkTime = 0.0f;
                        motionLinkCount = 0;

                        linkChange = false;
                        //�}�b�v�̃`�F���W���I��������Ƃ�MapManager�ɓ`����
                        parentMap.GetComponent<MapManager>().ChangeOff();

                        if (faceCount >= 4)
                        {
                            transform.rotation = Quaternion.Euler(180.0f, 0.0f, 0.0f);
                            faceCount = 1;
                        }
                        //���x�ύX
                        //if (state == scriptableObject.colorName) GetComponent<MeshRenderer>().material.color = Color.white * blockPlayerEmissive;
                        //else GetComponent<MeshRenderer>().material.color = Color.white * blockEmissive;
                    }
                    else if (motionLinkCount < changeMotionCount)
                    {
                        if (faceCount >= 4)
                        {
                            transform.Rotate(0.0f, 0.0f, 90.0f * 1.0f / changeMotionCount);
                        }
                        else if (faceCount >= 1)
                        {
                            transform.Rotate(90.0f * 1.0f / changeMotionCount, 0.0f, 0.0f);
                        }
                        currentLinkTime = 0.0f;
                        motionLinkCount++;
                    }
                }

            }
        }
    }

    public void CheckFloor()
	{
		rootCount = 0;
		List<GameObject> objList = new();

        bool goal = false;

		GameObject obj1 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z - 1);
		GameObject obj2 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z + 1);
		GameObject obj3 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
		GameObject obj4 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);


		if (obj1 != null)
		{
			if (parentMap.GetCheckedFloorList().Contains(obj1.GetComponent<Floor>()) == false)
			{ 
				if (obj1.GetComponent<Floor>().GetFloorState() == "goal")
				{
                    goal = true;
					parentMap.GetCheckedFloorList().Add(obj1.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
					Debug.Log("goal�ł�");
					rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.IsMapClear();
                    //�`�F�b�N�̏������I���܂ł̓v���C���[���[�V����������ҋ@������
                    parentMap.WaitOnGoal();
				}
			}
		}
		if (obj2 != null)
		{
			if (parentMap.GetCheckedFloorList().Contains(obj2.GetComponent<Floor>()) == false)
			{
				if (obj2.GetComponent<Floor>().GetFloorState() == "goal")
				{
                    goal = true;
                    parentMap.GetCheckedFloorList().Add(obj2.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
					Debug.Log("goal�ł�");
					rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.IsMapClear();
                    //�`�F�b�N�̏������I���܂ł̓v���C���[���[�V����������ҋ@������
                    parentMap.WaitOnGoal();

                }

			}
		}
		if (obj3 != null)
		{
			if (parentMap.GetCheckedFloorList().Contains(obj3.GetComponent<Floor>()) == false)
			{
				if (obj3.GetComponent<Floor>().GetFloorState() == "goal")
				{
                    goal = true;
                    parentMap.GetCheckedFloorList().Add(obj3.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
					Debug.Log("goal�ł�");
					rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.IsMapClear();
                    //�`�F�b�N�̏������I���܂ł̓v���C���[���[�V����������ҋ@������
                    parentMap.WaitOnGoal();

                }

			}
		}
		if (obj4 != null)
		{
			if (parentMap.GetCheckedFloorList().Contains(obj4.GetComponent<Floor>()) == false)
			{
				if (obj4.GetComponent<Floor>().GetFloorState() == "goal")
				{
                    goal = true;
                    parentMap.GetCheckedFloorList().Add(obj4.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^
					Debug.Log("goal�ł�");
					rootCount++;
                    //�S�[���������Ƃ��}�b�v�}�l�[�W���[�ɓ`����
                    parentMap.IsMapClear();
                    //�`�F�b�N�̏������I���܂ł̓v���C���[���[�V����������ҋ@������
                    parentMap.WaitOnGoal();

                }

			}
		}

        //�S�[���ɓ��B���ĂȂ�������`�F�b�N�������s
        if (goal == false)
        {
            if (obj1 != null)
            {
                if (parentMap.GetCheckedFloorList().Contains(obj1.GetComponent<Floor>()) == false)
                {

                    //Debug.Log("�T�����Ă���list�̒��ɂȂ������̂Ń`�F�b�N���܂�"
                    if (obj1.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
                    {
                        obj1.GetComponent<Floor>().SetOldFloor(this);
                        parentMap.GetCheckedFloorList().Add(obj1.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^

                        //root�̐��Ǝ����̏���o�^                                                      
                        rootCount++;
                        //�������s�����X�g�Ɋi�[
                        objList.Add(obj1);
                    }
                }
            }
            //Debug.Log("���𒲂ׂ܂�");
            if (obj2 != null)
            {
                if (parentMap.GetCheckedFloorList().Contains(obj2.GetComponent<Floor>()) == false)
                {
                    if (obj2.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
                    {
                        obj2.GetComponent<Floor>().SetOldFloor(this);
                        parentMap.GetCheckedFloorList().Add(obj2.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^;
                        rootCount++;
                        //�������s�����X�g�Ɋi�[
                        objList.Add(obj2);
                        Debug.Log(position);
                    }
                }
            }

            //Debug.Log("���𒲂ׂ܂�");
            if (obj3 != null)
            {
                if (parentMap.GetCheckedFloorList().Contains(obj3.GetComponent<Floor>()) == false)
                {
                    if (obj3.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
                    {
                        obj3.GetComponent<Floor>().SetOldFloor(this);
                        parentMap.GetCheckedFloorList().Add(obj3.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^

                        //root�̐��Ǝ����̏���o�^
                        rootCount++;
                        //�������s�����X�g�Ɋi�[
                        objList.Add(obj3);
                    }
                }
            }

            //Debug.Log("���𒲂ׂ܂�");
            if (obj4 != null)
            {
                if (parentMap.GetCheckedFloorList().Contains(obj4.GetComponent<Floor>()) == false)
                {
                    //�T�����Ă���list�̒��ɂȂ���ΐi��
                    if (obj4.GetComponent<Floor>().GetFloorState() == scriptableObject.colorName)
                    {
                        obj4.GetComponent<Floor>().SetOldFloor(this);
                        parentMap.GetCheckedFloorList().Add(obj4.GetComponent<Floor>());//�`�F�b�N����Floor��list�ɓo�^

                        //root�̐��Ǝ����̏���o�^
                        rootCount++;
                        //�������s�����X�g�Ɋi�[
                        objList.Add(obj4);
                    }
                }
            }
        }
		
		float wait = 0.01f;

        if (objList.Count == 1)
        {
            StartCoroutine(objList[0].GetComponent<Floor>().Check(wait));
        }
        else if (objList.Count == 2)
        {
            StartCoroutine(objList[0].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[1].GetComponent<Floor>().Check(wait));

        }
        else if (objList.Count == 3)
        {
            StartCoroutine(objList[0].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[1].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[2].GetComponent<Floor>().Check(wait));

        }
        else if (objList.Count == 4)
        {
            StartCoroutine(objList[0].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[1].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[2].GetComponent<Floor>().Check(wait));
            StartCoroutine(objList[3].GetComponent<Floor>().Check(wait));

        }

    }
    public void CheckOldRoot()
    {
        //root������Ȃ�������O�̏���root���m�F
        if (rootCount == 0)
        {
            Debug.Log("�߂�܂�");
            if (oldFloor != null) oldFloor.CheckOldRoot();

            Debug.Log(position.x);
            Debug.Log(position.z);
        }

        if (rootCount > 0)
        {
            rootCount--;

            if (rootCount == 0)
            {
                Debug.Log("�߂�܂�");
                if (oldFloor != null) oldFloor.CheckOldRoot();
                Debug.Log(position.x);
                Debug.Log(position.z);
            }
        }

    }

    public IEnumerator Check(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		CheckFloor();
	}
	
	//�G�t�F�N�g
	void CreateLinkEffect()
	{

		float side = 0.7f;

        GameObject obj1 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z - 1);
        GameObject obj2 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z + 1);
        GameObject obj3 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x - 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);
        GameObject obj4 = parentMap.GetGameObjectList().Find(match => match.GetComponent<Floor>().GetMapPosition().x == GetComponent<Floor>().GetMapPosition().x + 1 && match.GetComponent<Floor>().GetMapPosition().z == GetComponent<Floor>().GetMapPosition().z);

        GameObject linkEffectTop = null;
        GameObject linkEffectRight = null;
        GameObject linkEffectBottom = null;
        GameObject linkEffectLeft = null;


        if (obj1 != null) if(obj1.GetComponent<Floor>().GetFloorState() is  "player" or "goal") obj1 = null;
        if (obj2 != null) if (obj2.GetComponent<Floor>().GetFloorState() is "player" or "goal") obj2 = null;
        if (obj3 != null) if (obj3.GetComponent<Floor>().GetFloorState() is "player" or "goal") obj3 = null;
        if (obj4 != null) if (obj4.GetComponent<Floor>().GetFloorState() is "player" or "goal") obj4 = null;

        if (obj1 != null) linkEffectTop = Instantiate(linkEffectObject, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + side), Quaternion.identity) as GameObject;
        if (obj2 != null) linkEffectBottom = Instantiate(linkEffectObject, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - side), Quaternion.identity) as GameObject;
        if (obj3 != null) linkEffectLeft = Instantiate(linkEffectObject, new Vector3(transform.position.x - side, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        if (obj4 != null) linkEffectRight = Instantiate(linkEffectObject, new Vector3(transform.position.x + side, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

        if (linkEffectTop != null) linkEffectTop.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        if (linkEffectBottom != null) linkEffectBottom.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        if (linkEffectLeft != null) linkEffectLeft.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        if (linkEffectRight != null) linkEffectRight.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
    }
}
