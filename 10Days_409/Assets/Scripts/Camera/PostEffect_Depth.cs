using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect_Depth : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _depth = 0.125f;//深度ライン
    [SerializeField, Range(0.0f, 1.0f)] private float _near = 0.11f;//nearとの距離
    [SerializeField, Range(0.0f, 1.0f)] private float _middle = 0.1f;//middleとの距離
    [SerializeField, Range(0.0f, 0.1f)] private float _smoothOffset = 0.025f;//スムースステップの境界
    

    [SerializeField] private Material _shaderMaterial;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture depth1 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, source.format);//深度バッファ用
        RenderTexture depth2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, source.format);//深度バッファ用
        RenderTexture depth3 = RenderTexture.GetTemporary(source.width / 8, source.height / 8, 0, source.format);//深度バッファ用
        RenderTexture depth = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);//深度バッファ用
        RenderTexture normal = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);

        _shaderMaterial.SetFloat("_Depth", _depth);
        _shaderMaterial.SetFloat("_Near", _near);
        _shaderMaterial.SetFloat("_Middle", _middle);
        _shaderMaterial.SetFloat("_SmoothOffset", _smoothOffset);

        _shaderMaterial.SetTexture("_NormalTex", source);

        //sourceの情報をもとにshaderMaterialの0番目をかけたものをsに出力
        //被写体深度
        Graphics.Blit(source, depth1, _shaderMaterial, 0);//シェーダーの0番目のpassを使う
        Graphics.Blit(depth1, depth2, _shaderMaterial, 0);
        Graphics.Blit(depth2, depth3, _shaderMaterial, 0);
        Graphics.Blit(depth3, depth, _shaderMaterial, 0);

        Graphics.Blit(depth, normal, _shaderMaterial, 0);

        Graphics.Blit(normal, destination, _shaderMaterial, 1);

        RenderTexture.ReleaseTemporary(depth1);//テクスチャの開放
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
