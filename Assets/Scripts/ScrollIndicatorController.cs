using UnityEngine;
using UnityEngine.UI;

public class ScrollIndicatorController : MonoBehaviour
{
    public float maxHeight = 1500f; // ���� �� ����, Inspector���� ���� ����

    [SerializeField]
    private Image indicatorImage; // UI Image, Inspector���� �Ҵ�

    private RectTransform selfRectTransform;

    private void Start()
    {
        // UI ������Ʈ�� Rect Transform ��������
        selfRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // ���� UI ������Ʈ�� Height �� ��������
        float currentHeight = selfRectTransform.rect.height;

        // ���� ���� ������ �ִ� ���̸� ������ �̹����� �������ϰ�, �׷��� ������ �����ϰ�
        float alpha = currentHeight > maxHeight ? 1f : 0f;
        SetImageAlpha(alpha);
    }

    private void SetImageAlpha(float alpha)
    {
        Color imageColor = indicatorImage.color;
        imageColor.a = alpha;
        indicatorImage.color = imageColor;
    }
}
