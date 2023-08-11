using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{

    //����Ƽ���� �����ϴ� �Լ�, �±װ� �ٸ� ������Ʈ������ �浹 �� �����Ǵ� �Լ�
    //Ʈ���Ű� üũ�� �ݶ��̴����� ������ ��    
    void OnTriggerExit2D(Collider2D collision)
    {
        //Area �±װ� �ƴҰ�� return, �Ʒ� �Լ��� �������� �ʴ´�.
        if (!collision.CompareTag("Area"))
            return;

        //�÷��̾��� ��ġ�� ��(��)�� ��ġ�� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        // x,y �� ���� ���� ���� , ���밪�̿��� �ϹǷ� Mathf.Abs  
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // player �� ���� ���� ����, �밢���϶� 1.4 �̹Ƿ� -1 �Ǵ� 1�� �����
        Vector3 playerDir = GameManager.instance.player.inputVec; // 1 -1 1.4 
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        // swithch ~ case : ���� ���¿� ���� ������ �����ִ� Ű����
        switch (transform.tag)
        {
            // �׶��� �϶�, �� ������Ʈ�� �Ÿ� ���̿���
            // x��� y���� ���̸� ���ؼ� ����,���� �̵��ϴ� ����
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":

                break;
        }
    }
}

