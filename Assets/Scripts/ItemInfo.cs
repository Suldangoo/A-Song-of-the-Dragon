using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance;
    private InfoManager infoManager => InfoManager.Instance;

    [SerializeField] private Image weaponImage;
    [SerializeField] private Image armorImage;

    [Header("���� ����")]
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text weaponStatsText;
    [SerializeField] private TMP_Text weaponDescriptionText;

    [Header("�� ����")]
    [SerializeField] private TMP_Text armorNameText;
    [SerializeField] private TMP_Text armorStatsText;
    [SerializeField] private TMP_Text armorDescriptionText;

    public void UpdateItemInfo()
    {
        // 1. ���� ���� / �� ����
        int weaponIndex = SaveManager.Weapon;
        int armorIndex = SaveManager.Armor;

        if (weaponIndex >= 0 && weaponIndex < infoManager.weaponImages.Length)
        {
            weaponImage.sprite = infoManager.weaponImages[weaponIndex];
        }

        if (armorIndex >= 0 && armorIndex < infoManager.armorImages.Length)
        {
            armorImage.sprite = infoManager.armorImages[armorIndex];
        }

        // 2. ���� �̸� / �� �̸�
        string weaponName = itemManager.weaponDatas[weaponIndex].name;
        string armorName = itemManager.armorDatas[armorIndex].name;

        weaponNameText.text = $"{weaponName}";
        armorNameText.text = $"{armorName}";

        // 3. ���� ���� / �� ����
        int weaponFixedIncrease = itemManager.weaponDatas[weaponIndex].fixedIncrease;
        float weaponPercentIncrease = itemManager.weaponDatas[weaponIndex].percentIncrease;
        int armorFixedIncrease = itemManager.armorDatas[armorIndex].fixedIncrease;
        float armorPercentIncrease = itemManager.armorDatas[armorIndex].percentIncrease;

        string weaponStats = $"���ݷ�: {weaponFixedIncrease}\n�߰����ݷ�: +{((weaponPercentIncrease - 1) * 100).ToString("F0")}%";
        string armorStats = $"����: {armorFixedIncrease}\n�߰�����: +{((armorPercentIncrease - 1) * 100).ToString("F0")}%";

        weaponStatsText.text = weaponStats;
        armorStatsText.text = armorStats;

        // 4. ���� ���� / �� ����
        string weaponDescription = itemManager.weaponDatas[weaponIndex].description;
        string armorDescription = itemManager.armorDatas[armorIndex].description;

        weaponDescriptionText.text = weaponDescription;
        armorDescriptionText.text = armorDescription;
    }
}
