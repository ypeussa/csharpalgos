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

    public SLLNode Find(T data) {
        var it = head;
        while (it != null && !(it.data.Equals(data))) {
            it = it.next;
        }
        return it;
    }

    // not in C# LinkedList (not needed in DLL)
    public SLLNode FindNode(SLLNode node) {
        var it = head;
        while (it != null && it != node) {
            it = it.next;
        }
        return it;
    }

    // not in C# LinkedList (not needed in DLL)
    public SLLNode FindPreviousNode(SLLNode node) {
        var it = head;
        while (it != null && it.next != node) {
            it = it.next;
        }
        return (it != null && it.next == node) ? it : null;
    }

    // not in C# LinkedList
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

    public void AddStart(T value) {
		var newNode = new SLLNode();
		newNode.data = value;
		newNode.next = head;
		head = newNode;
    }

	public void AddLast(T value) {
		if (head == null) {
			Insert(0, value);
		} else {
			var last = head;
			while (last.next != null) {
				last = last.next;
			}
			var newNode = new SLLNode();
			newNode.data = value;
            newNode.next = null;
			last.next = newNode;
		}
	}

    public void AddAfter(SLLNode node, T value) {
        var newNode = new SLLNode();
        newNode.data = value;
        var it = FindNode(node);
        if (it != null) {
            newNode.next = it.next;
            it.next = newNode;
        } else
            throw new System.Exception("node not found");
    }
    
    public void AddBefore(SLLNode node, T value) {
        var newNode = new SLLNode();
        newNode.data = value;
        if (node == head) {
            newNode.next = head;
            head = newNode;
        } else {
            var it = FindPreviousNode(node);
            if (it != null) {
                newNode.next = it.next;
                it.next = newNode;
            } else {
                throw new System.Exception("node not found");
            }
        }
    }

    void Remove(SLLNode node) {
		if (node == head) {
			head = head.next;
		} else {
			var iter = FindPreviousNode(node);
			if (iter != null) {
				iter.next = node.next;
			} else {
				throw new System.InvalidOperationException("node not found");
			}
		}
    }

    public void Remove(T value) {
		if (head != null && head.data.Equals(value)) {
			head = head.next;
		} else {
			var node = head.next;
			var left = head;
			while (node != null) {
				if (node.data.Equals(value)) {
					left.next = node.next;
					return;
				}
				left = node;
				node = node.next;
			}
			throw new System.InvalidOperationException("node not found");
		}
    }

    // not in C# LinkedList (indices not preferred for LinkedList)
    public SLLNode GetNode(int i) {
        var it = head;
        while (i > 0) {
            it = it.next;
            i--;
        }
        return it;
    }
    // not in C# LinkedList (indices not preferred for LinkedList)
    public T GetValue(int i) {
        var it = GetNode(i);
        return it.data;
    }
    // not in C# LinkedList (indices not preferred for LinkedList)
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
