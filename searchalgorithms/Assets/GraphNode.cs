using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NodeType { Clear, Obstacle, Start, Goal };

public class GraphNode : MonoBehaviour {
	public NodeType type;
	public List<GraphNode> neighbors;

	void OnMouseDown() {
		GameObject.Find ("Grid").GetComponent<Grid> ().AdjustNodeType (this);
	}

	public void SetColorAccordingToType() {
		var colors = new Color[] { Color.white, Color.gray, Color.red, Color.blue };
		GetComponent<Renderer> ().material.color = colors [(int)type];
	}
}
