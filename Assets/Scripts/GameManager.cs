using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [Header("레벨 관련 설정")]
    public Slider EXPBar; // 경험치 바
    public float BaseMaxEXP; // 최초 레벨업에 필요한 경험치
    public float LevelUpMultiplier; // 레벨업할 때마다 늘어나는 경험치 배수

    [Header("스테이지 관련 설정")]
    public TextMeshProUGUI KillCountText; // 킬카운트 관련 텍스트
    public TextMeshProUGUI TimerText; // 타이머 텍스트
    public GameObject GameOverPanel;// 게임오버 패널
    public Button RestartButton; // 재시작 버튼
    public Button QuitButton; // 나가기 버튼

    private float currentExp; // 현재 경험치
    private float maxExp; // 레벨업까지 필요한 경험치
    private int level; // 레벨
    private int killCount; // 킬카운트
    private float timer; // 타이머

    private void Start()
    {
        // 초기화
        maxExp = BaseMaxEXP;
        currentExp = 0f;
        level = 1;
        killCount = 0;
        timer = 0;

        // 버튼들과 함수를 연결
        RestartButton.onClick.AddListener(Restart);
        QuitButton.onClick.AddListener(Quit);

        // 경험치 바를 업데이트하는 함수 호출
        UpdateUI();
    }
    private void Update()
    {
        StartTimer();
    }

    /// <summary>
    /// 게임을 재시작 하는 버튼
    /// </summary>
    private void Restart()
    {
        // 시간 복구
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 프로그램 종료 버튼
    /// </summary>
    private void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 게임오버 되었을때 호출되는 함수
    /// </summary>
    public void GameOver()
    {
        // 시간을 멈춤
        Time.timeScale = 0f;

        // 게임오버 패널을 켜줌.
        GameOverPanel.SetActive(true);
    }


    /// <summary>
    /// 타이머 관련 동작을 진행할 함수
    /// </summary>
    private void StartTimer()
    {
        // 델타타임으로 시간 측정
        timer += Time.deltaTime;

        // 현재 측정된 시간에서 60초로 나누면 분, 나머지연산은 초
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // 포맷에 맞춰서 넣어줌.string.Format("{0:첫번째 매개변수}:{1:두번째 매개변수}",minutes,seconds);
        TimerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    /// <summary>
    /// 적이 죽었을때 킬카운트를 추가해줄 함수
    /// </summary>
    public void AddKillCount()
    {
        // 킬카운트를 하나 올려주면 된다.
        killCount++;
        // 킬카운트 업데이트
        KillCountText.text = $"Kill : {killCount}";
       // KillCountText.text = killCount.ToString();
    }

    /// <summary>
    /// 경험치를 늘려주는 함수
    /// </summary>
    /// <param name="exp"></param>
    public void GetEXP(float exp)
    {
        // 현재 경험치에 매개변수로 받은 경험치만큼 더해주고,
        currentExp += exp;
        // 만약 레벨업에 필요한 경험치만큼을 모았으면 레벨업
        if(currentExp >= maxExp)
        {
            LevelUp();
        }
        // UI 업데이트
        UpdateUI();
    }

    /// <summary>
    /// 레벨업 해주는 함수
    /// </summary>
    private void LevelUp()
    {
        // 레벨을 올려줌.
        level++;
        // 현재 경험치를 0으로 만들어주어야함.
        currentExp = 0f;
        // 최대 경험치(레벨업 경험치)를 올려주어야함.
        maxExp *= LevelUpMultiplier;

        // 레벨업했을때 스킬추가 UI 나오기
        Debug.Log($"현재 레벨 : {level}");
    }

    /// <summary>
    /// 경험치 바를 업데이트하는 함수
    /// </summary>
    private void UpdateUI()
    {
        // 경험치바 업데이트
        EXPBar.value = currentExp / maxExp;

    }

}
