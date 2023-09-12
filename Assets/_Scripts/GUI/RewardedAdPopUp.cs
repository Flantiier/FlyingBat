using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdPopUp : MonoBehaviour
{
	#region Variables
	[Header("References")]
	[SerializeField] private Slider slider;
	[SerializeField] private TextMeshProUGUI countdownText;

	[Header("PopUp properties")]
	[SerializeField] private float showDuration = 3f;
    private float _timer;
    #endregion

    #region Builts_In
    private void FixedUpdate()
    {
        HandlePopUp();
    }

    private void OnEnable()
    {
        _timer = showDuration;
        slider.value = _timer;
    }
    #endregion

    #region Methods
    private void HandlePopUp()
    {
        if(_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
            countdownText.text = $"Closing in {Mathf.CeilToInt(_timer)}.";
            slider.value = _timer / showDuration;
        }
        else
        {
            Debug.Log("close pop up");
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }
	#endregion
}
