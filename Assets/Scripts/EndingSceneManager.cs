using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneManager : MonoBehaviour
{
    SceneChanger sceneChanger => SceneChanger.Instance;

    public Image illustrationImage; // ���ѷα� �Ϸ���Ʈ �̹���
    public TMP_Text prologueText; // ���ѷα� �ؽ�Ʈ
    public Image startButtonImage; // ���� ���� ��ư �̹���
    public TMP_Text startButtonText; // ���� ���� ��ư �ؽ�Ʈ

    private void Start()
    {
        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 1�� ���

        StartCoroutine(PrologueSequence());
    }

    public void OnClickTitle()
    {
        sceneChanger.SceneChange("Title");
    }

    private IEnumerator PrologueSequence()
    {
        // �Ϸ���Ʈ ��Ÿ����
        yield return FadeInImage(illustrationImage, 1f);

        // ���ѷα� �ؽ�Ʈ ��Ÿ����
        yield return ShowPrologueText();

        // ���� ���� ��ư ��Ÿ����
        yield return FadeInImage(startButtonImage, 1f);
        yield return FadeInText(startButtonText, 1f);

        // �ڷ�ƾ�� ��� ����� �Ŀ� ��ư Interactable Ȱ��ȭ
        startButtonImage.GetComponent<Button>().interactable = true;
    }

    private IEnumerator FadeInImage(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            image.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = targetColor;
    }

    private IEnumerator FadeInText(TMP_Text text, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            text.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = targetColor;
    }

    private IEnumerator ShowPrologueText()
    {
        string prologueContent = "������ �ϰ� ��� �� ���, ���Ϲ�Ʈ��\n" + SaveManager.NickName + "�� �տ� ������ߴ�.\n\n" +
            "���Ϲ�Ʈ�� ��� �ٸ� ��鿡 ����\n���� ���� �ƴϾ�����, �״� �ʿ�����μ�\n" +
            "���� ������ ���迡 ȥ���� �������� �ǵ���\n����ġ��, ������ ������ ��Ű�� ���̾���.\n\n" +
            "�׷��� ���Ϲ�Ʈ�� ��°�� �����ߴ���,\n" +
            "�̷�Ƽ�� ����� ������ ��°�� �ı��ߴ�����\n�� ������ ���� ���Ѵ�.\n\n" +
            SaveManager.NickName + "��(��) ���Ϲ�Ʈ�� ����Ʈ���� ��,\n���Ϲ�Ʈ�� ������ �긮�� �־���.\n" +
            SaveManager.NickName + "��(��) ���Ϲ�Ʈ�� ����� ������\n������ ������ ���������� ������ �Ӹ�޾�����,\n" +
            "��°�� ���Ϲ�Ʈ�� ������ ��ȴ����� ���� ���Ѵ�.";

        prologueText.text = "";

        foreach (char letter in prologueContent)
        {
            prologueText.text += letter;
            yield return new WaitForSeconds(0.05f); // �� ���ھ� ��Ÿ���� ������
        }
    }
}
