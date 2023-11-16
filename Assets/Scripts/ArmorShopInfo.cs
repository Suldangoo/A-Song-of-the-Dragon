using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorShopInfo : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance;
    private InfoManager infoManager => InfoManager.Instance;

    [Header("���� �������� ���� ����")]
    [SerializeField] private Image currentArmorImage;
    [SerializeField] private TMP_Text currentArmorNameText;
    [SerializeField] private TMP_Text currentArmorStatsText;
    [SerializeField] private TMP_Text currentArmorDescriptionText;

    [Header("�����Ϸ��� ���� ����")]
    [SerializeField] private Image purchaseArmorImage;
    [SerializeField] private TMP_Text purchaseArmorNameText;
    [SerializeField] private TMP_Text purchaseArmorStatsText;
    [SerializeField] private TMP_Text purchaseArmorDescriptionText;
    [SerializeField] private TMP_Text purchaseArmorCostText;

    private int itemCode;

    public void UpdateShopInfo(int itemIndex)
    {
        itemCode = itemIndex;

        // ���� �������� �� ���� ����
        UpdateArmorInfo(SaveManager.Armor, currentArmorImage, currentArmorNameText, currentArmorStatsText, currentArmorDescriptionText);

        // �����Ϸ��� �� ���� ����
        UpdateArmorInfo(itemCode, purchaseArmorImage, purchaseArmorNameText, purchaseArmorStatsText, purchaseArmorDescriptionText);
        purchaseArmorCostText.text = $"���: {itemManager.armorDatas[itemCode].cost}";
    }

    private void UpdateArmorInfo(int ArmorIndex, Image image, TMP_Text nameText, TMP_Text statsText, TMP_Text descriptionText)
    {
        if (ArmorIndex >= 0 && ArmorIndex < infoManager.armorImages.Length)
        {
            image.sprite = infoManager.armorImages[ArmorIndex];
        }

        string ArmorName = itemManager.armorDatas[ArmorIndex].name;
        nameText.text = $"{ArmorName}";

        int ArmorFixedIncrease = itemManager.armorDatas[ArmorIndex].fixedIncrease;
        float ArmorPercentIncrease = itemManager.armorDatas[ArmorIndex].percentIncrease;

        string ArmorStats = $"����: {ArmorFixedIncrease}\n�߰�����: +{((ArmorPercentIncrease - 1) * 100).ToString("F0")}%";
        statsText.text = ArmorStats;

        string ArmorDescription = itemManager.armorDatas[ArmorIndex].description;
        descriptionText.text = ArmorDescription;
    }

    public void OnClickPurchase()
    {
        // ���� �� Ȯ��
        int currentGold = SaveManager.Gold;
        int purchaseCost = itemManager.armorDatas[itemCode].cost;

        // ���� ����ϸ� ���⸦ ����
        if (currentGold >= purchaseCost)
        {
            // �� ����
            SaveManager.Gold -= purchaseCost;

            // ���� ��ü
            SaveManager.Armor = itemCode;

            // ���� â �ݱ�
            gameObject.SetActive(false);

            // ���� ���� ����
            InfoManager.Instance.UpdateInfo();

            // ���� ���� �Ϸ� �̺�Ʈ ����
            EventManager.Instance.ShowEvent(7);
        }
        else
        {
            // ���� �����ϸ� �˸�
            Debug.Log("���� �����մϴ�.");
        }
    }
}
