using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    Image timerImage;
    TextMeshProUGUI timerDisplay;

    [SerializeField] float countdownTime;
    public event System.Action GameStartAction;

    void Awake()
    {
        timerImage = GetComponent<Image>();
        timerDisplay = GetComponentInChildren<TextMeshProUGUI>();

        FindObjectOfType<InstructionsMenu>().GameStartAction += OnGameStart;
    }

    void OnGameStart()
    {
        gameObject.SetActive(true);
    }

    IEnumerator Start()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            yield return new WaitForEndOfFrame();
            currentTime -= Time.deltaTime;

        }
    }
}