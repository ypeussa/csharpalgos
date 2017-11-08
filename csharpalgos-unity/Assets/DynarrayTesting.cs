using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynarrayTesting : MonoBehaviour {

    void Start() {
        //TestGetSet();
        //TestInsert();
        TestRemove();
    }

    void TestGetSet() {
        print("Testing get/set");
        Dynarray squares = new Dynarray(3);
        squares.Set(0, 1);
        squares.Set(1, 4);
        squares.Set(2, 9);
        print(squares.Get(1));
        DebugPrintDynarray(squares);
        print("expected 4, then [ 1 4 9 ]");
    }
    void TestInsert() {
        print("Testing insert/add");
        Dynarray hundreds = new Dynarray(0);
        DebugPrintDynarray(hundreds);
        hundreds.Insert(0, 100);
        hundreds.Insert(1, 200);
        hundreds.Add(300);
        hundreds.Add(400);
        DebugPrintDynarray(hundreds);
        print("inserts/adds at end - expected [ 100 200 300 400 ]");
        hundreds.Insert(0, 500);
        DebugPrintDynarray(hundreds);
        print("insert at start - expected [ 500 100 200 300 400 ]");
        hundreds.Insert(1, 800);
        DebugPrintDynarray(hundreds);
        print("insert in middle - expected [ 500 800 100 200 300 400 ]");
    }

    void TestRemove() {
        print("Testing remove");
        Dynarray values = DebugCreateLittleArray();
        DebugPrintDynarray(values);
        print("Remove 3");
        values.Remove(3);
        DebugPrintDynarray(values);
        print("Remove 0");
        values.Remove(0);
        DebugPrintDynarray(values);
        print("Remove Length() - 1");
        values.Remove(values.Length() - 1);
        DebugPrintDynarray(values);
    }

    Dynarray DebugCreateLittleArray() {
        // 0 5 10 15 20
        var d = new Dynarray(0);
        for (int i = 0; i < 5; i++)
            d.Add(i * 5);
        return d;
    }
    void DebugPrintDynarray(Dynarray d) {
        var data = d.DebugGetRawData();
        var s = "[";
        foreach (int x in data)
            s += " " + x;
        print(s + " ]");
    }

}
