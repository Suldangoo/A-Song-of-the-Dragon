using UnityEngine;
using TMPro;

public class SetStatus : MonoBehaviour
{
    // �ν����Ϳ��� ������ TMpro �ؽ�Ʈ�� ��ư ������Ʈ
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI wisdomText;
    [SerializeField] private TextMeshProUGUI charmText;
    [SerializeField] private TextMeshProUGUI remainingPointsText;

    [SerializeField] private GameObject[] decreaseButtons; // ���߱� ��ư��
    [SerializeField] private GameObject[] increaseButtons; // ���̱� ��ư��

    [SerializeField] private int minStatus = 5;     // �ּ� ����
    [SerializeField] private int maxStatus = 15;    // �ִ� ����

    // �ʱ� �������ͽ� ��
    [SerializeField] private int[] stats = { 5, 5, 5, 5, 5 }; // ��, ��ø, �ǰ�, ����, �ŷ�
    [SerializeField] private int remainingPoints = 15; // �ʱ� ���� ����Ʈ

    private void Start()
    {
        UpdateUI();
    }

    // Ư�� �������ͽ��� ���ҽ�Ű�� �޼���
    public void DownStatus(int index)
    {
        if (stats[index] > minStatus)
        {
            stats[index]--;
            remainingPoints++;
            UpdateUI();
        }
    }

    // Ư�� �������ͽ��� ������Ű�� �޼���
    public void UpStatus(int index)
    {
        if (stats[index] < maxStatus && remainingPoints > 0)
        {
            stats[index]++;
            remainingPoints--;
            UpdateUI();
        }
    }

    // UI ������ ���� �޼���
    private void UpdateUI()
    {
        strengthText.text = stats[0].ToString();
        agilityText.text = stats[1].ToString();
        healthText.text = stats[2].ToString();
        wisdomText.text = stats[3].ToString();
        charmText.text = stats[4].ToString();
        remainingPointsText.text = remainingPoints.ToString();

        // ���߱� / ���̱� ��ư ���� ����
        for (int i = 0; i < 5; i++)
        {
            decreaseButtons[i].SetActive(stats[i] > minStatus);
            increaseButtons[i].SetActive(stats[i] < maxStatus && remainingPoints > 0);
        }
    }
}
