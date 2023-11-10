using System.Collections;
using UnityEngine;

public class LogoMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;  // ������ �Ÿ�
    public float moveSpeed = 1.0f;     // �����̴� �ӵ�
    public float bounceHeight = 0.1f;  // ƨ��� ����

    private Vector3 originalPosition;  // �ʱ� ��ġ

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveLogo());
    }

    IEnumerator MoveLogo()
    {
        while (true)
        {
            float t = 0f;

            // �Ʒ��� �̵�
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;

                float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = originalPosition + new Vector3(0f, yOffset, 0f);

                yield return null;
            }

            t = 0f;

            // ���� �̵�
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;

                float yOffset = Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = originalPosition - new Vector3(0f, yOffset, 0f);

                yield return null;
            }
        }
    }
}
