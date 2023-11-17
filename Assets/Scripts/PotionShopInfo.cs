using TMPro;
using UnityEngine;

public class PotionShopInfo : MonoBehaviour
{
    InfoManager InfoManager => InfoManager.Instance;

    [SerializeField] private TextMeshProUGUI smallCostText;
    [SerializeField] private TextMeshProUGUI bigCostText;

    private int smallCost = 100;
    private int bigCost = 500;

    private void OnEnable()
    {
        // Charm�� ���� ��� ����
        int charmBonus = SaveManager.Charm; // ��������� ����

        // ���� ��뿡 Charm �ݿ�
        smallCost -= charmBonus;
        bigCost -= charmBonus;

        // UI�� ���
        smallCostText.text = $"{smallCost}���";
        bigCostText.text = $"{bigCost}���";
    }

    public void OnClickBuyPotion(int size)
    {
        int cost = 0;
        int maxQuantity = 0;

        // ���� ����
        if (size == 0)
        {
            cost = smallCost;
            maxQuantity = 3;
        }
        // ���� ����
        else if (size == 1)
        {
            cost = bigCost;
            maxQuantity = 3;
        }

        // ������ ���� �������� Ȯ��
        if (CanBuyPotion(size, cost, maxQuantity))
        {
            BuyPotion(size, cost, maxQuantity);
        }
        else
        {
            // ���� �Ұ� �޽��� �Ǵ� �ٸ� ó���� ���⿡ �߰�
            Debug.Log("������ ���� �Ұ����մϴ�.");
        }
    }

    private bool CanBuyPotion(int size, int cost, int maxQuantity)
    {
        // �÷��̾��� ���� ���� ���� ���� Ȯ��
        int playerGold = SaveManager.Gold;
        int currentQuantity = (size == 0) ? SaveManager.SmallHpPotion : SaveManager.LargeHpPotion;

        return playerGold >= cost && currentQuantity < maxQuantity;
    }

    private void BuyPotion(int size, int cost, int maxQuantity)
    {
        // ���� ��� ����
        SaveManager.Gold -= cost;

        // ���� ���� ����
        if (size == 0)
        {
            SaveManager.SmallHpPotion++;
        }
        // ���� ���� ����
        else if (size == 1)
        {
            SaveManager.LargeHpPotion++;
        }

        // �� ����
        InfoManager.UpdateInfo();
    }
}
