using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS : MonoBehaviour {
	Grid grid;
	static List<GraphNode> visualizeSearched;

	static bool JustSearch(GraphNode startNode) {
		if (startNode == null)
			Debug.LogError ("Trying to run BFS with a null start node!");

		// Implementation straight from
		// http://en.wikipedia.org/wiki/Breadth-first_search

		var Q = new Queue<GraphNode>();
		Q.Enqueue (startNode);

		var discovered = new HashSet<GraphNode>();
		discovered.Add(startNode);

		while (Q.Count > 0) {
			var v = Q.Dequeue();
			// Process v.
			// If we're doing simple search we can just check if v is a goal node,
			// and return true if so. This also means a path exists from start to goal.
			if (v.type == NodeType.Goal)
				return true;
			foreach (GraphNode w in v.neighbors) {
				// Slight addition to the BFS on Wikipedia: we have chosen to mark some
				// nodes as obstacles that the search is not allowed to go into, so we
				// have to check to make sure the newly found node is _not_ an obstacle.
				// If we wanted to use the exact Wikipedia code, we'd have to remove the
				// obstacle nodes from the graph before we run BFS.
				if (!discovered.Contains(w) && w.type != NodeType.Obstacle) {
					Q.Enqueue(w);
					discovered.Add(w);
				}
			}
		}
		    return false; // We went through the whole graph, no goal nodes to be found.
	}

	// Helper function that builds us the final path for the pathfinding BFS below
	static List<GraphNode> BuildPathFromBFSData(Dictionary<GraphNode, GraphNode> discovered,
	                                            GraphNode goalFound) {
		var path = new List<GraphNode> ();
		path.Add(goalFound);
		GraphNode previousNode = discovered [goalFound];
		while (previousNode != null) {
			path.Add (previousNode);
			previousNode = discovered [previousNode];
		}
		path.Reverse ();
		return path;
	}

	// Search for goal and return path to goal if found, otherwise return null.
	// Only difference to pure search is that we do not just store the info of which nodes
	// we have visited, but also where we came from when we first visited them.
	// That lets us build a path after we hit a goal node.
	static List<GraphNode> SearchAndBuildPath(GraphNode startNode) {
		if (startNode == null)
			Debug.LogError ("Trying to run BFS with a null start node!");
		
		var Q = new Queue<GraphNode>();
		Q.Enqueue (startNode);
		
		var discovered = new Dictionary<GraphNode, GraphNode>();
		discovered.Add(startNode, null);
		
		while (Q.Count > 0) {
			var v = Q.Dequeue();
			// Process v.
			if (v.type == NodeType.Goal)
				return BuildPathFromBFSData(discovered, v);
			foreach (GraphNode w in v.neighbors) {
				if (!discovered.ContainsKey(w) && w.type != NodeType.Obstacle) {
					Q.Enqueue(w);
					discovered.Add(w, v);
					visualizeSearched.Add(w); // to show how the search proceeds
				}
			}
		}
		return null; // We went through the whole graph, no goal nodes to be found.
	}

	// Find a start node from the scene. We're assuming there's only one.
	static GraphNode FindStartNode() {
		var graphNodes = GameObject.FindGameObjectsWithTag ("GraphNode");
		GraphNode startNode = null;
		foreach (GameObject go in graphNodes) {
			var gn = go.GetComponent<GraphNode>();
			if (gn.type == NodeType.Start) {
				startNode = gn;
				break;
			}
		}
		return startNode;
	}

	void Start() {
		grid = GameObject.Find ("Grid").GetComponent<Grid> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			visualizeSearched = new List<GraphNode>();

			var startNode = FindStartNode();
			bool goalFound = JustSearch(startNode);
			Debug.Log ("Goal found: " + goalFound);

			var path = SearchAndBuildPath(startNode);
			grid.ClearVisualization();
//			if (visualizeSearched != null)
//				grid.VisualizeNodeGroupAnimated(visualizeSearched, Color.green, 0.3f);
			if (visualizeSearched != null)
				grid.VisualizeNodeGroup(visualizeSearched, Color.green);
			if (path != null)
				grid.VisualizeNodeGroup(path, Color.black);
		}
	}
}
