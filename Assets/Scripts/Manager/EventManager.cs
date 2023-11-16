using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Event
{
    public string title; // �̺�Ʈ ���� (�ν����Ϳ� ǥ�ÿ�)
    public Sprite illustration; // �Ϸ���Ʈ �̹���
    [TextArea(1, 50)] public string text; // �ؽ�Ʈ ����
    public string[] buttonTexts; // ��ư �ؽ�Ʈ �迭
    public UnityEvent[] buttonEvents; // ��ư Ŭ�� �� �߻��� �̺�Ʈ �迭
}

public class EventManager : MonoBehaviour
{
    #region �̱���
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();
            }
            return instance;
        }
    }
    private static EventManager instance;
    #endregion

    // ���� �߻��ϴ� �̺�Ʈ
    public int nowEvent = 0; // ���� �⺻���� 0�� �̺�Ʈ (���� �̺�Ʈ) �� ����ȴ�.

    // ���ӿ� �����ϴ� �̺�Ʈ��
    public Event[] events;

    // ȭ���� UI�� ǥ�õǴ� ������Ʈ��
    [SerializeField] private Image illustrationImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text button1;
    [SerializeField] private TMP_Text button2;
    [SerializeField] private TMP_Text button3;
    [SerializeField] private TMP_Text button4;

    private void Start()
    {
        // ���� ���� �� ù ��° �̺�Ʈ ����
        ShowEvent(nowEvent);
    }

    public void ShowEvent(int eventIndex)
    {
        if (eventIndex < events.Length)
        {
            Event currentEvent = events[eventIndex];

            // illustrationImage, text ���� UI ��ҿ� currentEvent�� ������ ����
            illustrationImage.sprite = currentEvent.illustration;
            illustrationImage.gameObject.SetActive(currentEvent.illustration != null);

            text.text = currentEvent.text;

            // �� ��ư �ؽ�Ʈ �� �̺�Ʈ ����
            SetButton(button1, currentEvent.buttonTexts.Length > 0 ? currentEvent.buttonTexts[0] : "", currentEvent.buttonEvents.Length > 0 ? currentEvent.buttonEvents[0] : null);
            SetButton(button2, currentEvent.buttonTexts.Length > 1 ? currentEvent.buttonTexts[1] : "", currentEvent.buttonEvents.Length > 1 ? currentEvent.buttonEvents[1] : null);
            SetButton(button3, currentEvent.buttonTexts.Length > 2 ? currentEvent.buttonTexts[2] : "", currentEvent.buttonEvents.Length > 2 ? currentEvent.buttonEvents[2] : null);
            SetButton(button4, currentEvent.buttonTexts.Length > 3 ? currentEvent.buttonTexts[3] : "", currentEvent.buttonEvents.Length > 3 ? currentEvent.buttonEvents[3] : null);
        }
    }

    private void SetButton(TMP_Text buttonText, string text, UnityEvent onClickEvent)
    {
        // ���� �κп� '�� ' �߰�
        string formattedText = $"�� {text}";
        buttonText.text = formattedText;

        // UITextInteraction ��ũ��Ʈ�� �߰��ϰ� onClickEvent�� �Ҵ�
        UITextInteraction textInteraction = buttonText.gameObject.GetComponent<UITextInteraction>();
        if (textInteraction == null)
        {
            textInteraction = buttonText.gameObject.AddComponent<UITextInteraction>();
        }
        textInteraction.onClickEvent = onClickEvent;

        // SetActive�� �ؽ�Ʈ�� ������� ���� ��쿡�� True�� ����
        buttonText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    public void ChangeEvent(int eventIndex)
    {
        nowEvent = eventIndex;
        ShowEvent(nowEvent);
    }
}
