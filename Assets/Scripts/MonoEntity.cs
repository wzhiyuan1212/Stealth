using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
	Player = 1,
	AI = 2,
}

public class MonoEntity : MonoBehaviour {
	public EntityType entityType;
}
