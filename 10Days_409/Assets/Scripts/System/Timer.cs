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

    //�ݒ���ŏ��̏�Ԃɖ߂�
    public void Reset() {
        currentTime = 0f;
    }

    //�J�E���g�A�b�v�^�C�������~�b�g�^�C���𒴂�����
    public bool IsTime() {
        return (currentTime >= limitTime);
    }

    //���~�b�g�^�C����ݒ肷��
    public void SetLimitTime(float sec) {
        limitTime = sec;
    }

    //���~�b�g�^�C����Ԃ�
    public float GetLimitTime() {
        return limitTime;
    }

    //�J�E���g�A�b�v�^�C���̋����ݒ�
    //�f�o�b�O�p�r�ɂ̂ݎg�p
    public void SetCurrentTime(float sec) {
        currentTime = sec;
    }

    //���݂̃J�E���g�A�b�v�^�C����Ԃ�
    public float GetCountUpTime() {
        return currentTime;
    }

    //���݂̃J�E���g�_�E���^�C����Ԃ�
    public float GetCountDownTime() {
        return (limitTime - currentTime);
    }

    //���~�b�g�^�C���ɑ΂��ẴJ�E���g�A�b�v�^�C���̔䗦
    public float Rate() {
        return (currentTime / limitTime);
    }

    //���݂̃^�C�}�[�����~�b�g��Ԃɂ���
    public void Overlimit() {
        currentTime = limitTime;
    }
}
