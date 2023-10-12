using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
      coll = GetComponent<Collider2D>();
    }

    //����Ƽ���� �����ϴ� �Լ�, �±װ� �ٸ� ������Ʈ������ �浹 �� �����Ǵ� �Լ�
    //Ʈ���Ű� üũ�� �ݶ��̴����� ������ ��    
    void OnTriggerExit2D(Collider2D collision)
    {
        //Area �±װ� �ƴҰ�� return, �Ʒ� �Լ��� �������� �ʴ´�.
        if (!collision.CompareTag("Area"))
            return;

        //�÷��̾��� ��ġ�� ���� ��ġ�� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
             
        // swithch ~ case : ���� ���¿� ���� ������ �����ִ� Ű����
        switch (transform.tag)
        {
            // �±װ� �׶��� - �� ������Ʈ�� �Ÿ� ���̿���
            // x��� y���� ���̸� ���ؼ� ����,���� �̵��ϴ� ����
            case "Ground":
                // x,y �� ���� ���� ���� , ���밪�̿��� �ϹǷ� Mathf.Abs  
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                // player �� ���� ���� ����, �밢���϶� 1.4 �̹Ƿ� -1 �Ǵ� 1�� �����
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

                // �±װ� ���ʹ�
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(ran + dist * 2);
                }

                break;
        }
    }
}

