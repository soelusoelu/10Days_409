using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect_Depth : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _depth = 0.125f;//深度ライン
    [SerializeField, Range(0.0f, 1.0f)] private float _near = 0.3f;//nearの距離
    [SerializeField, Range(0.0f, 1.0f)] private float _far = 0.8f;//farの値
    [SerializeField, Range(0.0f, 1.0f)] private float _smoothOffset = 0.03f;//スムースステップの境界
    [SerializeField, Range(0.01f, 1.00f)] private float _pickCount = 0.1f;//for文を回す量


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
        RenderTexture depth = RenderTexture.GetTemporary(source.width / 16, source.height / 16, 0, source.format);//深度バッファ用
        RenderTexture normal = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);//ブルームを掛けた画像を入れるための関数

        _shaderMaterial.SetFloat("_Depth", _depth);
        _shaderMaterial.SetFloat("_Near", _near);
        _shaderMaterial.SetFloat("_Far", _far);
        _shaderMaterial.SetFloat("_SmoothOffset", _smoothOffset);
        _shaderMaterial.SetFloat("_PickCount", _pickCount);


        _shaderMaterial.SetTexture("_NormalTex", normal);//_NormalTexにnormalを入れる

        //sourceの情報をもとにshaderMaterialの0番目をかけたものをsに出力
        //被写体深度
        Graphics.Blit(source, depth1);//ダウンサンプリング
        Graphics.Blit(depth1, depth2);//ダウンサンプリング
        Graphics.Blit(depth2, depth3);//ダウンサンプリング
        Graphics.Blit(depth3, depth); //ダウンサンプリング

        //深度に対してブルームを掛けた画像を出力
        Graphics.Blit(depth, normal, _shaderMaterial, 0);
        //Graphics.Blit(depth, destination, _shaderMaterial, 0);//テスト用

        //元の画像とブルームを掛けた画像を足し合わせる
        Graphics.Blit(source, destination, _shaderMaterial, 1);

        RenderTexture.ReleaseTemporary(depth1);//テクスチャの開放
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
