using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour {
	private static EntityManager instance = null;
	private Hashtable entityMap;
	void Awake(){
		entityMap = new Hashtable ();
		instance = this;
	}
	public static EntityManager Instance{
		get{
			return instance;
		}
	}
	public void registerEntity(GameObject newEntity, EntityType type){
			entityMap.Add (type, newEntity);
	}
	public void removeEntity(EntityType type){
		entityMap.Remove (type);
	}
	public GameObject getEntityFromId(EntityType type){
		if (entityMap.ContainsKey (type)) {
			return (GameObject)entityMap[type];
		}
		return null;
	}
}
