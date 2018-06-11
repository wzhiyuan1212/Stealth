using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAIManager : MonoBehaviour {
	public static MonoAIManager Instance;
	public MonoAI[] aiList;

	private void Awake()
	{
		Instance = this;

		return;
	}

	public void OnDetectEnemy()
	{
		foreach(MonoAI ai in aiList)
		{
			ai.OnDetectEnemy();
		}
		return;
	}

	public void OnLevelEnd(bool isWin)
	{
		foreach(MonoAI ai in aiList)
		{
			ai.SetEnable(false);
		}

		MonoUIManager.Instance.OnLevelEnd(isWin);

		return;
	}
}
