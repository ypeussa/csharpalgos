public class SmartDynarray {
    int[] data;
    int capacity;
    int used;

    public SmartDynarray(int size) {
        capacity = 2;
        while (capacity < size)
            capacity *= 2;
        data = new int[capacity];
        used = size;
    }

    public int Length() {
        return used;
    }

    public int Get(int index) {
        if (index >= used)
            throw new System.IndexOutOfRangeException();
        return data[index];
    }

    public void Set(int index, int value) {
        if (index >= used)
            throw new System.IndexOutOfRangeException();
        data[index] = value;
    }

    public void Insert(int index, int value) {
        used++;
        if (capacity >= used) {
            for (int i = used-2; i >= index; i--) {
                data[i+1] = data[i];
            }
        } else {
            capacity *= 2;
            int[] newData = new int[capacity];
            for (int i = 0; i < used-1; i++) {
                newData[i + (i < index ? 0 : 1)] = data[i];
            }
            data = newData;
        }
        data[index] = value;
    }

    public void Add(int value) {
        Insert(Length(), value);
    }

    public void Remove(int index) {
 
    }

    public int[] DebugGetRawData() {
        return data;
    }
}
