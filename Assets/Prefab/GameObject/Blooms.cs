using UnityEngine;

public class Blooms : MonoBehaviour
{
    public Color outlineColor = Color.yellow; // �֊s�̐F
    public float outlineWidth = 0.03f;        // �֊s�̑���

    private Material outlineMaterial;

    void Start()
    {
        // ���̃}�e���A�����擾���A�A�E�g���C���}�e���A�����쐬
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            outlineMaterial = new Material(Shader.Find("Custom/OutlineShader"));
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
            outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);

            // ���b�V���ɃA�E�g���C����ǉ�
            renderer.materials = new Material[] { renderer.material, outlineMaterial };
        }
    }
}
