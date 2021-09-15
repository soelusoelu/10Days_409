using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float currentTime = 0f;
    private float limitTime = 0f;

    public void Update() {
        currentTime += Time.deltaTime;
    }

    //設定を最初の状態に戻す
    public void Reset() {
        currentTime = 0f;
    }

    //カウントアップタイムがリミットタイムを超えたか
    public bool IsTime() {
        return (currentTime >= limitTime);
    }

    //リミットタイムを設定する
    public void SetLimitTime(float sec) {
        limitTime = sec;
    }

    //リミットタイムを返す
    public float GetLimitTime() {
        return limitTime;
    }

    //カウントアップタイムの強制設定
    //デバッグ用途にのみ使用
    public void SetCurrentTime(float sec) {
        currentTime = sec;
    }

    //現在のカウントアップタイムを返す
    public float GetCountUpTime() {
        return currentTime;
    }

    //現在のカウントダウンタイムを返す
    public float GetCountDownTime() {
        return (limitTime - currentTime);
    }

    //リミットタイムに対してのカウントアップタイムの比率
    public float Rate() {
        return (currentTime / limitTime);
    }

    //現在のタイマーをリミット状態にする
    public void Overlimit() {
        currentTime = limitTime;
    }
}
