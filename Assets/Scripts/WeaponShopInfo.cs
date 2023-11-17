using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShopInfo : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance;
    private InfoManager infoManager => InfoManager.Instance;

    [Header("���� �������� ���� ����")]
    [SerializeField] private Image currentWeaponImage;
    [SerializeField] private TMP_Text currentWeaponNameText;
    [SerializeField] private TMP_Text currentWeaponStatsText;
    [SerializeField] private TMP_Text currentWeaponDescriptionText;

    [Header("�����Ϸ��� ���� ����")]
    [SerializeField] private Image purchaseWeaponImage;
    [SerializeField] private TMP_Text purchaseWeaponNameText;
    [SerializeField] private TMP_Text purchaseWeaponStatsText;
    [SerializeField] private TMP_Text purchaseWeaponDescriptionText;
    [SerializeField] private TMP_Text purchaseWeaponCostText;

    private int itemCode;

    public void UpdateShopInfo(int itemIndex)
    {
        itemCode = itemIndex;

        // ���� �������� ���� ���� ����
        UpdateWeaponInfo(SaveManager.Weapon, currentWeaponImage, currentWeaponNameText, currentWeaponStatsText, currentWeaponDescriptionText);

        // �����Ϸ��� ���� ���� ����
        UpdateWeaponInfo(itemCode, purchaseWeaponImage, purchaseWeaponNameText, purchaseWeaponStatsText, purchaseWeaponDescriptionText);
        purchaseWeaponCostText.text = $"���: {itemManager.weaponDatas[itemCode].cost - SaveManager.Charm}";
    }

    private void UpdateWeaponInfo(int weaponIndex, Image image, TMP_Text nameText, TMP_Text statsText, TMP_Text descriptionText)
    {
        if (weaponIndex >= 0 && weaponIndex < infoManager.weaponImages.Length)
        {
            image.sprite = infoManager.weaponImages[weaponIndex];
        }

        string weaponName = itemManager.weaponDatas[weaponIndex].name;
        nameText.text = $"{weaponName}";

        int weaponFixedIncrease = itemManager.weaponDatas[weaponIndex].fixedIncrease;
        float weaponPercentIncrease = itemManager.weaponDatas[weaponIndex].percentIncrease;

        string weaponStats = $"���ݷ�: {weaponFixedIncrease}\n�߰����ݷ�: +{((weaponPercentIncrease - 1) * 100).ToString("F0")}%";
        statsText.text = weaponStats;

        string weaponDescription = itemManager.weaponDatas[weaponIndex].description;
        descriptionText.text = weaponDescription;
    }

    public void OnClickPurchase()
    {
        // ���� �� Ȯ��
        int currentGold = SaveManager.Gold;
        int purchaseCost = itemManager.weaponDatas[itemCode].cost;

        // Charm�� ���� ��� ����
        int adjustedCost = Mathf.Max(0, purchaseCost - SaveManager.Charm); // ��������� ����

        // ���� ����ϸ� ���⸦ ����
        if (currentGold >= adjustedCost)
        {
            // �� ����
            SaveManager.Gold -= adjustedCost;

            // ���� ��ü
            SaveManager.Weapon = itemCode;

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
