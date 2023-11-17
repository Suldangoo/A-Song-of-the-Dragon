using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name; // ���� �̸�
    public int fixedIncrease; // ���� ��� ��ġ
    public float percentIncrease; // �ۼ�Ʈ ��� ��ġ
    public int cost; // ���
    [TextArea(2, 5)] public string description; // ���� ���� (2�� �̻�, 5�� ����)
}

[System.Serializable]
public class ArmorData
{
    public string name; // �� �̸�
    public int fixedIncrease; // ���� ��� ��ġ
    public float percentIncrease; // �ۼ�Ʈ ��� ��ġ
    public int cost; // ���
    [TextArea(2, 5)] public string description; // �� ���� (2�� �̻�, 5�� ����)
}

public class ItemManager : MonoBehaviour
{
    #region �̱���
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
            }
            return instance;
        }
    }
    private static ItemManager instance;
    #endregion

    public WeaponData[] weaponDatas; // ���� ������ �迭
    public ArmorData[] armorDatas; // �� ������ �迭

    InfoManager infoManager => InfoManager.Instance;

    // ������ ����ϴ� �޼ҵ�
    public void OnClickPotion(int size)
    {
        int maxHp = SaveManager.Health * 3;

        // ���� ���� ���
        if (size == 0 && SaveManager.SmallHpPotion > 0)
        {
            SaveManager.SmallHpPotion--;
            int healAmount = Mathf.RoundToInt(maxHp * 0.3f);
            SaveManager.Hp += healAmount;
            SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);
            infoManager.UpdateInfo(); // ���� ����
        }

        // ū ���� ���
        else if (size == 1 && SaveManager.LargeHpPotion > 0)
        {
            SaveManager.LargeHpPotion--;
            int healAmount = Mathf.RoundToInt(maxHp * 0.8f);
            SaveManager.Hp += healAmount;
            SaveManager.Hp = Mathf.Min(SaveManager.Hp, maxHp);
            infoManager.UpdateInfo(); // ���� ����
        }
    }
}