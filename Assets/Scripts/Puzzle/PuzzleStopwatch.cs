using UnityEngine;
using TMPro;

public class PuzzleStopwatch : MonoBehaviour
{
    [Header("Timer Display")]
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private float completionTime = 0f;

    private bool isRunning = false;
    private bool isStopped = false;


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
        if (!isRunning || isStopped)
            return;

        elapsedTime += Time.deltaTime;

        UpdateDisplay();
    }


    // ====================================================
    // START TIMER
    // ====================================================

    public void StartTimer()
    {
        if (isStopped)
            return;

        isRunning = true;

        Debug.Log("⏱ NEW STOPWATCH STARTED");
    }


    // ====================================================
    // STOP TIMER + SAVE COMPLETION TIME
    // ====================================================

    public void StopTimer()
    {
        if (isStopped)
            return;

        isRunning = false;
        isStopped = true;

        // SAVE FINAL COMPLETION TIME
        completionTime = elapsedTime;

        UpdateDisplay();

        Debug.Log(
            "⏱ COMPLETION TIME SAVED: " +
            FormatTime(completionTime)
        );
    }


    // ====================================================
    // DISPLAY
    // ====================================================

    private void UpdateDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(elapsedTime);
        }
    }


    // ====================================================
    // GET COMPLETION TIME
    // ====================================================

    public float GetCompletionTime()
    {
        return completionTime;
    }


    // ====================================================
    // GET CURRENT TIME
    // ====================================================

    public float GetElapsedTime()
    {
        return elapsedTime;
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
        return isStopped;
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
}