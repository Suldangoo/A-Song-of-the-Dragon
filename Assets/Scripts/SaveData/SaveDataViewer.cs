using UnityEngine;

// 디버그 용 클래스
// save data를 에디터에서 볼수 있도록 함 
public class SaveDataViewer : MonoBehaviour
{
    #region 싱글톤
    public static SaveDataViewer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveDataViewer>();
            }
            return instance;
        }
    }
    private static SaveDataViewer instance;
    #endregion

    public bool ShowTutorial;
    public int BestScore;
    public float bgmVolume;
    public float sfxVolume;

    private void Awake()
    {
        // 모든 씬에서 하나만 유지
        if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        GetSave();
    }

    // 저장 데이터 가져와 표시
    public void GetSave()
    {
        ShowTutorial = SaveManager.ShowTutorial;
        BestScore = SaveManager.BestScore;
        bgmVolume = SaveManager.BgmVolume;
        sfxVolume = SaveManager.SfxVolume;
    }

    // 현재 표시된 데이터로 저장 데이터 수정
    public void SetSave()
    {
        SaveManager.ShowTutorial = ShowTutorial;
        SaveManager.BestScore = BestScore;
        SaveManager.BgmVolume = bgmVolume;
        SaveManager.SfxVolume = sfxVolume;
    }

    // 세이브 데이터 삭제 (초기화)
    public void ClearSave()
    {
        SaveManager.ClearData();
    }
}
