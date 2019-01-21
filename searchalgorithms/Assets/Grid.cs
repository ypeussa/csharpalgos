using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {
	public int x, y; // size of grid; must call BuildGrid after changes

	public GameObject gridNodePrefab;
	public GameObject visualizationQuadPrefab;
	public NodeType nextNodeTypeToSetOnClick; // public for debug

	[SerializeField]
	GameObject[] grid;
	[SerializeField]
	GameObject[] visualizationLayer;

	float animationTime;
	float animationTimestep;
	Color visualizationColor;
	List<GraphNode> nodesToVisualize;
	int nextNodeToShow;

	[MenuItem ("Algorithms/GenerateGrid")]
	static void GenerateGrid() {
		GameObject.Find ("Grid").GetComponent<Grid> ().BuildGrid ();
	}

	public void AdjustNodeType(GraphNode n) {
		n.type = nextNodeTypeToSetOnClick;
		n.SetColorAccordingToType();
	}

	public void ClearVisualization() {
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				visualizationLayer[i + j * x].GetComponent<Renderer>().material.color = Color.clear;
			}
		}	
	}

	public void VisualizeNodeGroup(List<GraphNode> nodes, Color c) {
		//VisualizeNodeGroupAnimated(nodes, c, 0.0f);
		foreach (GraphNode n in nodes) {
			int index = ArrayUtility.IndexOf(grid, n.gameObject);
			visualizationLayer[index].GetComponent<Renderer>().material.color = c;
		}
	}

	public void VisualizeNodeGroupAnimated(List<GraphNode> nodes, Color c, float timestep) {
		animationTime = 0.0f;
		animationTimestep = timestep;
		visualizationColor = c;
		nodesToVisualize = nodes;
		nextNodeToShow = 0;
		// TODO: change color one by one in Update()
	}

	void BuildGrid() {
		// clear old grid

		// why doesn't this work? only finds half of the children...?
//		foreach (Transform child in transform) {
//			print("foo");
//			DestroyImmediate(child.gameObject);
//		}
		// anyway, this works correctly (as long as there are no other graphs in scene!)
		var graphNodes = GameObject.FindGameObjectsWithTag ("GraphNode");
		foreach (GameObject g in graphNodes) {
			DestroyImmediate (g);
		}
		var quads = GameObject.FindGameObjectsWithTag ("VisualQuad");
		foreach (GameObject g in quads) {
			DestroyImmediate (g);
		}

		// create new grid to desired size, move objects to position
		grid = new GameObject[x * y];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				var go = Instantiate<GameObject> (gridNodePrefab);
				grid [i + j * x] = go;
				go.transform.position = new Vector3 (-x * 0.5f + i, 0.0f, -y * 0.5f + j);
				go.transform.parent = transform;
			}
		}
		// build the graph: get all neighbors that are possible for a given node on the grid
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				var	 gn = grid[i + j * x].GetComponent<GraphNode>();
				if (j > 0) { // up
					gn.neighbors.Add(grid[i + (j-1) * x].GetComponent<GraphNode>());
				}
				if (i < x-1) { // right
					gn.neighbors.Add(grid[i+1 + j * x].GetComponent<GraphNode>());
				}
				if (j < y-1) { // down
					gn.neighbors.Add(grid[i + (j+1) * x].GetComponent<GraphNode>());
				}
				if (i > 0) { // left
					gn.neighbors.Add(grid[i-1 + j * x].GetComponent<GraphNode>());
				}
			}
		}
		// build visualization grid
		visualizationLayer = new GameObject[x * y];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				var go = Instantiate<GameObject> (visualizationQuadPrefab);
				visualizationLayer[i + j * x] = go;
				go.transform.position = new Vector3 (-x * 0.5f + i, 0.6f, -y * 0.5f + j);
				go.transform.parent = transform;
			}
		}
	}
	
	void Update() {
		var editModeKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

		for (int i = 0; i < editModeKeys.Length; i++) {
			if (Input.GetKeyDown (editModeKeys [i])) {
				nextNodeTypeToSetOnClick = (NodeType)i;
			}
		}
		if (nodesToVisualize != null) { // animation active
			animationTime += Time.deltaTime;
			while (animationTime > animationTimestep) {
				animationTime -= animationTimestep;
				if (nextNodeToShow < nodesToVisualize.Count) {
					int index = ArrayUtility.IndexOf(grid, nodesToVisualize[nextNodeToShow].gameObject);
					visualizationLayer[index].GetComponent<Renderer>().material.color = visualizationColor;
					nextNodeToShow++;
				} else {
					nodesToVisualize = null; // end animation
				}
			}
		}
	}
}
