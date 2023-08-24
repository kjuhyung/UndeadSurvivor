using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // ����, ���̾�, ��ĵ ��� �迭, ���� ����� ��ǥ ���� ����
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll
            (transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
             // Distance(A,B) : ���� A �� B �� �Ÿ��� ������ִ� �Լ�

            if (curDiff < diff) // �ݺ����� ���� ������ �Ÿ���
                                // ����� �Ÿ����� ������ ��ü
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
