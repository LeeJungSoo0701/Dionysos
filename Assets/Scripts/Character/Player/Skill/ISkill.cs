using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool CanUse { get; set; }
    float CoolTime { get; set; }
    float RemainTime { get; set; }

    //�������̽������� ������ ���� �ǰ� �ʱ�ȭ�� �ȵǱ⶧����
    void Work(Player player);
}
