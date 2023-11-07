using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISkill
{
    public float _coolTime; 
    public bool CanUse { get; set; }
    public float coolTime { get; set; }
    public float maxTime { get; set; }
    public bool powerUp { get; set; }

    void Start()
    {
        coolTime = _coolTime;
    }
    void Update()
    {
        if (!CanUse)
        {
            if (coolTime <= _coolTime)
            {
                coolTime += Time.deltaTime;
            }
        }
    }
    public void Work(Player player)
    {
        if (CanUse)
        {
            if(powerUp)
            {
                player.dashPowerUp = true;
            }
            else
            {
                player.dashPowerUp = false;
            }
            StartCoroutine(DashLogic(player));
        }
    }
    IEnumerator DashLogic(Player player)
    {
        if ((player.vertical == 0f) && (player.horizontal == 0f))
        {
            if (player.isFlip)
                player.target = new Vector3(transform.position.x + player.DashDistance, transform.position.y, transform.position.z);
            else
                player.target = new Vector3(transform.position.x - player.DashDistance, transform.position.y, transform.position.z);
        }
        else
        {
            if (player.vertical > 0f && player.horizontal == 0f)//��
                player.target = new Vector3(transform.position.x, transform.position.y, transform.position.z + player.DashDistance);
            else if (player.vertical > 0f && player.horizontal > 0)//��
                player.target = new Vector3(transform.position.x + player.DashDistance, transform.position.y, transform.position.z + player.DashDistance);
            else if (player.vertical == 0f && player.horizontal > 0)//��
                player.target = new Vector3(transform.position.x + player.DashDistance, transform.position.y, transform.position.z);
            else if (player.vertical < 0f && player.horizontal > 0)//��
                player.target = new Vector3(transform.position.x + player.DashDistance, transform.position.y, transform.position.z - player.DashDistance);
            else if (player.vertical < 0f && player.horizontal == 0f)//��
                player.target = new Vector3(transform.position.x, transform.position.y, transform.position.z - player.DashDistance);
            else if (player.vertical < 0f && player.horizontal < 0)//��
                player.target = new Vector3(transform.position.x - player.DashDistance, transform.position.y, transform.position.z - player.DashDistance);
            else if (player.vertical == 0f && player.horizontal < 0)//��
                player.target = new Vector3(transform.position.x - player.DashDistance, transform.position.y, transform.position.z);
            else if(player.vertical > 0f && player.horizontal < 0) //��
                player.target = new Vector3(transform.position.x - player.DashDistance, transform.position.y, transform.position.z + player.DashDistance);
            
        }

        float elapseTime = 0;
        while (elapseTime < 1)
        {
            elapseTime += Time.deltaTime;
            if(player.dash)
                transform.position = Vector3.Lerp(transform.position, player.target, elapseTime / 2);

            if (!player.dash || Vector3.Distance(player.target, transform.position) < 0.01f)
            {
                CanUse = false;
                player.dash = false;
                coolTime = 0;

                yield break;
            }
            yield return null;
        }
        yield return null;
    }

}
