using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect_Bloom : MonoBehaviour
{
    [SerializeField, Range(1, 30)] private int _iteration = 1;
    [SerializeField, Range(0.0f, 1.0f)] private float _threshold = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _softThreshold = 0.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _intensity = 1.0f;
    [SerializeField] bool _debug;
    

    //4�E���T���v�����O���ĐF�����}�e���A��
    [SerializeField] private Material _shaderMaterial;
    private RenderTexture[] _renderTextures = new RenderTexture[30];

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var filterParams = Vector4.zero;
        var knee = _threshold * _softThreshold;
        filterParams.x = _threshold;
        filterParams.y = _threshold - knee;
        filterParams.z = knee * 2.0f;
        filterParams.w = 0.25f / (knee + 0.00001f);
        _shaderMaterial.SetVector("_FilterParams", filterParams);
        _shaderMaterial.SetFloat("_Intensity", _intensity);
        _shaderMaterial.SetTexture("_SourceTex", source);

        var width = source.width;
        var height = source.height;
        var currentSource = source;

        var pathIndex = 0;
        var i = 0;
        RenderTexture currentDest = null;

        // �_�E���T���v�����O
        for (; i < _iteration; i++)
        {
            width /= 2;
            height /= 2;
            if (width < 2 || height < 2)
            {
                break;
            }
            currentDest = _renderTextures[i] = RenderTexture.GetTemporary(width, height, 0, source.format);

            // �ŏ��̈��͖��x���o�p�̃p�X���g���ă_�E���T���v�����O����
            pathIndex = (i == 0) ? 0 : 1;
            Graphics.Blit(currentSource, currentDest, _shaderMaterial, pathIndex);

            currentSource = currentDest;
        }

        // �A�b�v�T���v�����O
        for (i -= 2; i >= 0; i--)
        {
            currentDest = _renderTextures[i];

            // Blit���Ƀ}�e���A���ƃp�X���w�肷��
            Graphics.Blit(currentSource, currentDest, _shaderMaterial, 2);

            _renderTextures[i] = null;
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDest;
        }

        // �Ō��destination��Blit
        pathIndex = _debug ? 4 : 3;
        Graphics.Blit(currentSource, destination, _shaderMaterial, pathIndex);
        RenderTexture.ReleaseTemporary(currentSource);//�e�N�X�`���̊J��
    }
}
