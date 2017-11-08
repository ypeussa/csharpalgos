// Generic linked list
// YP 8.11.2017
// untested
// no sentinels, does not keep track of size
// compare to C# default DLL:
// https://msdn.microsoft.com/en-us/library/he2s3bh7(v=vs.110).aspx
// https://www.dotnetperls.com/linkedlist
// TODO: constructor taking ienumerable -> can construct from array
// TODO: support ienumerable -> foreach loop works on SLL
// TODO: write tests for SLL
// TODO: add double-linked list
// TODO: stack, queue (using DLL, using array, ...)

public class SingleLinkedList<T> {
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


    // AddAfter
    // AddBefore

    void Remove(SLLNode node) {

    }

    void Remove(T value) {

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

}
