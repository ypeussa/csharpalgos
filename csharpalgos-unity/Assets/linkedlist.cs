// Generic linked list
// YP 8.11.2017
// untested
// no sentinels, does not keep track of size
// compare to C# default DLL:
// https://msdn.microsoft.com/en-us/library/he2s3bh7(v=vs.110).aspx
// https://www.dotnetperls.com/linkedlist

// TODO: write more tests for SLL
// TODO: add double-linked list
// TODO: stack, queue (using DLL, using array, ...)

using System.Collections.Generic;

public class SingleLinkedList<T> : IEnumerable<T> {
    public class SLLNode {
        public SLLNode next;
        public T data;
    }

    SLLNode head;

    public SLLNode First {
        get
        {
            return head;
        }
    }

    public int Count {
        get
        {
            int n = 0;
            var it = head;
            while (it != null) {
                n++;
                it = it.next;
            }
            return n;
        }
    }

	public SingleLinkedList() { }

	public SingleLinkedList(IEnumerable<T> collection) {
		var iter = collection.GetEnumerator();
		while (iter.MoveNext()) {
			AddLast(iter.Current);
		}
	}

	#region IEnumerable implementation

	public IEnumerator<T> GetEnumerator() {
		if (head != null) {
			var node = head; 
			while (node != null) {
				yield return node.data;
				node = node.next;
			}
		}
	}

	#endregion

	#region IEnumerable implementation

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		return this.GetEnumerator();
	}

	#endregion

    SLLNode Find(T data) {
        var it = head;
        while (it != null && !(it.data.Equals(data))) {
            it = it.next;
        }
        return it;
    }

    // not in C# linked list
    public void Insert(int index, T data) {
        var newNode = new SLLNode();
        newNode.data = data;
        if (index == 0) {
            newNode.next = head;
            head = newNode;
        } else {
            var left = GetNode(index - 1);
            var right = left.next;
            left.next = newNode;
            newNode.next = right;
        }
    }

	public void AddLast(T value) {
		if (head == null) {
			Insert(0, value);
		} else {
			var last = GetNode(Count - 1);
			var newNode = new SLLNode();
			newNode.data = value;
			last.next = newNode;
		}
	}

	//TODO: Had an odd bug with after...
	//public void AddAfter(SLLNode node, T value)

	public void AddBefore(SLLNode node, T value) {
		var iterNode = head;
		var newNode = new SLLNode();
		newNode.data = value;
		if (iterNode.data.Equals(node.data)) {
			newNode.next = head;
			head = newNode;
		} else {
			var left = iterNode;
			for (int i = 0; i < Count; i++) {
				if (node.Equals(iterNode.data)) {
					left.next = node;
					node.next = iterNode;
					return;
				}
				left = iterNode;
				iterNode = iterNode.next;
			}
		}
	}

    void Remove(SLLNode node) {
		var iterNode = head;
		for (int i = 0; i < Count && node != null; i++) {
			if (node.Equals(iterNode.data)) {
				Remove(i);
				return;
			}
			iterNode = iterNode.next;
		}
    }

    public void Remove(T value) {
		var node = head;
		for (int i = 0; i < Count && node != null; i++) {
			if (value.Equals(node.data)) {
				Remove(i);
				return;
			}
			node = node.next;
		}
    }

    // not in C# linked list - exercise?
    public SLLNode GetNode(int i) {
        var it = head;
        while (i > 0) {
            it = it.next;
            i--;
        }
        return it;
    }
    // not in C# linked list - exercise?
    public T GetValue(int i) {
        var it = GetNode(i);
        return it.data;
    }
    // not in C# linked list - exercise?
    void Remove(int index) {
        if (index == 0) {
            head = head.next;
        } else {
            var left = GetNode(index - 1);
            left.next = left.next.next;
        }
    }

	public override string ToString() {
		var str = "[";
		if (head != null) {
			str += head.data;
			var node = head.next; 
			while (node != null) {
				str += " " + node.data;
				node = node.next;
			}
		}
		return str + "]";
	}

}
