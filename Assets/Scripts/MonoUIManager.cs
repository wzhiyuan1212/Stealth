using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonoUIManager : MonoBehaviour {
	public static MonoUIManager Instance;

	private void Awake()
	{
		Instance = this;

		return;
	}

	public GameObject panel;
	public Text resultText;
	public Button restartBtn;

	// Use this for initialization
	void Start () {
		panel.gameObject.SetActive(false);
		restartBtn.onClick.RemoveAllListeners();
		restartBtn.onClick.AddListener(OnRestartBtnClick);

		return;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnRestartBtnClick()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		return;
	}

	public void OnLevelEnd(bool isWin)
	{
		panel.gameObject.SetActive(true);
		resultText.text = isWin ? "You Win" : "You lose";

		return;
	}
}
