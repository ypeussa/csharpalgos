using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLinkedListTesting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TestIndexOperations();
		TestConstructor();
		TestAddRemove();
		TestAddBeforeAfter();
		TestEnumeration();
		TestAddStart();
	}
	
	void TestIndexOperations() {
		var sll = new SingleLinkedList<string>();
		print("Test index");
		sll.Insert(0, "Foo");
		sll.Insert(1, "Bar");
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 2, Value: Bar");
		print(sll);
	}

	void TestConstructor() {
		print("Test Constructor");
		var sll = new SingleLinkedList<string>(new string[] {"Foo", "Bar"});
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 2, Value: Bar");
		print(sll);
	}

	void TestAddRemove() {
		var sll = new SingleLinkedList<string>();
		print("Test Add");
		sll.AddLast("Foo");
		sll.AddLast("Bar");
		sll.AddLast("Baz");
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 3, Value: Bar");
		print(sll);
		print("Test Remove");
		sll.Remove("Bar");
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 2, Value: Baz");
		print(sll);
	}

	void TestAddBeforeAfter() {
		var sll = new SingleLinkedList<string>();
		print("Test Add Before/After");
		sll.AddLast("Baz");
		sll.AddBefore(sll.First, "Foo");
		sll.AddBefore(sll.GetNode(1), "Bar");
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 3, Value: Bar");
		print(sll);
	}


	void TestEnumeration() {
		print("Test Enumeration");
		var sll = new SingleLinkedList<string>(new string[] {"Foo", "Bar", "Baz"});
		foreach (var item in sll) {
			print(item);
		}
		print("Expected: Foo, Bar, Baz");
	}

	void TestAddStart() {
		var sll = new SingleLinkedList<string>();
		print("Test Add Start");
		sll.AddStart("Bar");
		sll.AddStart("Foo");
		print("Count: " + sll.Count + ", Value at 1: " + sll.GetValue(1));
		print("Expected Count: 2, Value: Bar");
		print(sll);
	}

}