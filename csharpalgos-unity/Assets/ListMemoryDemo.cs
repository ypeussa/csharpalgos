using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMemoryDemo : MonoBehaviour {

    public List<int> list;

    void Start () {
        print("testing list memory behavior");
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.Add(1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
        list.RemoveAt(list.Count - 1);
    }

    void Update () {
        		
	}
}
