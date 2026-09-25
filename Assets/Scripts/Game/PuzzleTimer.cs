using UnityEngine;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    [Header("Timer Text")]
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private float completionTime = 0f;

    private bool isRunning = false;
    private bool timerCompleted = false;


    // ====================================================
    // START
    // ====================================================

    private void Start()
    {
        StartTimer();
    }


    // ====================================================
    // UPDATE
    // ====================================================

    private void Update()
    {
        if (!isRunning)
        {
            Debug.Log("⛔ TIMER UPDATE BLOCKED");
            return;
        }

        if (timerCompleted)
        {
            Debug.Log("⛔ TIMER COMPLETED - UPDATE BLOCKED");
            return;
        }

        elapsedTime += Time.deltaTime;

        UpdateTimerDisplay();
    }


    // ====================================================
    // START TIMER
    // ====================================================

    public void StartTimer()
    {
        if (timerCompleted)
            return;

        isRunning = true;

        UpdateTimerDisplay();

        Debug.Log("⏱ Timer Started");
    }


    // ====================================================
    // STOP TIMER
    // ====================================================

    public void StopTimer()
    {
        if (timerCompleted)
            return;

        // STOP TIMER IMMEDIATELY
        isRunning = false;

        // SAVE FINAL TIME
        completionTime = elapsedTime;

        // MARK TIMER AS COMPLETED
        timerCompleted = true;

        // SHOW FINAL TIME
        if (timerText != null)
        {
            timerText.text = FormatTime(completionTime);
        }

        Debug.Log(
            "⏱ Timer Stopped at: " +
            FormatTime(completionTime)
        );

        Debug.Log("⛔ STOP FROM PUZZLETIMER 4");

        // HARD STOP
        // Prevent Update() from running again.
        enabled = false;
    }


    // ====================================================
    // RESET TIMER
    // ====================================================

    public void ResetTimer()
{
    enabled = true;

    elapsedTime = 0f;
    completionTime = 0f;

    timerCompleted = false;
    isRunning = true;

    UpdateTimerDisplay();

    Debug.Log("⏱ Timer Reset");
}

    // ====================================================
    // DISPLAY TIMER
    // ====================================================

    private void UpdateTimerDisplay()
    {
        if (timerText == null)
            return;

        if (timerCompleted)
        {
            timerText.text = FormatTime(completionTime);
            return;
        }

        timerText.text = FormatTime(elapsedTime);
    }


    // ====================================================
    // FORMAT TIME
    // ====================================================

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return string.Format(
            "{0:00}:{1:00}",
            minutes,
            seconds
        );
    }


    // ====================================================
    // GET CURRENT TIME
    // ====================================================

    public float GetElapsedTime()
    {
        return elapsedTime;
    }


    // ====================================================
    // GET COMPLETION TIME
    // ====================================================

    public float GetCompletionTime()
    {
        return completionTime;
    }


    // ====================================================
    // CHECK TIMER
    // ====================================================

    public bool IsRunning()
    {
        return isRunning;
    }


    // ====================================================
    // CHECK COMPLETED
    // ====================================================

    public bool IsCompleted()
    {
        return timerCompleted;
    }
}