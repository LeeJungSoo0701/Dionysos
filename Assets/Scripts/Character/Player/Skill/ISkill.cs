using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    //string RhythmState { get; set; }
    bool CanUse { get; set; }
    float CoolTime { get; set; }
    float RemainTime { get; set; }
    bool powerUp { get; set; }

    //�������̽������� ������ ���� �ǰ� �ʱ�ȭ�� �ȵǱ⶧����
    void Work(Player player);
}
