using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect_Depth : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _depth = 0.125f;//�[�x���C��
    [SerializeField, Range(0.0f, 1.0f)] private float _near = 0.11f;//near�Ƃ̋���
    [SerializeField, Range(0.0f, 1.0f)] private float _middle = 0.1f;//middle�Ƃ̋���
    [SerializeField, Range(0.0f, 0.1f)] private float _smoothOffset = 0.025f;//�X���[�X�X�e�b�v�̋��E
    

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
        RenderTexture depth = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);//�[�x�o�b�t�@�p
        RenderTexture normal = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

        _shaderMaterial.SetFloat("_Depth", _depth);
        _shaderMaterial.SetFloat("_Near", _near);
        _shaderMaterial.SetFloat("_Middle", _middle);
        _shaderMaterial.SetFloat("_SmoothOffset", _smoothOffset);

        _shaderMaterial.SetTexture("_NormalTex", source);

        //source�̏������Ƃ�shaderMaterial��0�Ԗڂ����������̂�s�ɏo��
        //��ʑ̐[�x
        Graphics.Blit(source, depth1, _shaderMaterial, 0);//�V�F�[�_�[��0�Ԗڂ�pass���g��
        Graphics.Blit(depth1, depth2, _shaderMaterial, 0);
        Graphics.Blit(depth2, depth3, _shaderMaterial, 0);
        Graphics.Blit(depth3, depth, _shaderMaterial, 0);

        Graphics.Blit(depth, normal, _shaderMaterial, 0);

        Graphics.Blit(normal, destination, _shaderMaterial, 1);

        RenderTexture.ReleaseTemporary(depth1);//�e�N�X�`���̊J��
        RenderTexture.ReleaseTemporary(depth2);
        RenderTexture.ReleaseTemporary(depth3);
        RenderTexture.ReleaseTemporary(depth);
        RenderTexture.ReleaseTemporary(normal);
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
