using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect_Depth : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _depth = 0.125f;//�[�x���C��
    [SerializeField, Range(0.0f, 1.0f)] private float _near = 0.3f;//near�̋���
    [SerializeField, Range(0.0f, 1.0f)] private float _far = 0.8f;//far�̒l
    [SerializeField, Range(0.0f, 1.0f)] private float _smoothOffset = 0.03f;//�X���[�X�X�e�b�v�̋��E
    [SerializeField, Range(0.01f, 1.00f)] private float _pickCount = 0.1f;//for�����񂷗�


    [SerializeField] private Material _shaderMaterial;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        RenderTexture depth1 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, source.format);//�[�x�o�b�t�@�p
        RenderTexture depth2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, source.format);//�[�x�o�b�t�@�p
        RenderTexture depth3 = RenderTexture.GetTemporary(source.width / 8, source.height / 8, 0, source.format);//�[�x�o�b�t�@�p
        RenderTexture depth = RenderTexture.GetTemporary(source.width / 16, source.height / 16, 0, source.format);//�[�x�o�b�t�@�p
        RenderTexture normal = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);//�u���[�����|�����摜�����邽�߂̊֐�

        _shaderMaterial.SetFloat("_Depth", _depth);
        _shaderMaterial.SetFloat("_Near", _near);
        _shaderMaterial.SetFloat("_Far", _far);
        _shaderMaterial.SetFloat("_SmoothOffset", _smoothOffset);
        _shaderMaterial.SetFloat("_PickCount", _pickCount);


        _shaderMaterial.SetTexture("_NormalTex", normal);//_NormalTex��normal������

        //source�̏������Ƃ�shaderMaterial��0�Ԗڂ����������̂�s�ɏo��
        //��ʑ̐[�x
        Graphics.Blit(source, depth1);//�_�E���T���v�����O
        Graphics.Blit(depth1, depth2);//�_�E���T���v�����O
        Graphics.Blit(depth2, depth3);//�_�E���T���v�����O
        Graphics.Blit(depth3, depth); //�_�E���T���v�����O

        //�[�x�ɑ΂��ău���[�����|�����摜���o��
        Graphics.Blit(depth, normal, _shaderMaterial, 0);
        //Graphics.Blit(depth, destination, _shaderMaterial, 0);//�e�X�g�p

        //���̉摜�ƃu���[�����|�����摜�𑫂����킹��
        Graphics.Blit(source, destination, _shaderMaterial, 1);

        RenderTexture.ReleaseTemporary(depth1);//�e�N�X�`���̊J��
        RenderTexture.ReleaseTemporary(depth2);
        RenderTexture.ReleaseTemporary(depth3);
        RenderTexture.ReleaseTemporary(depth);
        RenderTexture.ReleaseTemporary(normal);

        //Graphics.Blit(source, destination, _shaderMaterial, 2);
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
