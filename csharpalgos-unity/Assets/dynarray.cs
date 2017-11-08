
public class Dynarray {
    int[] data;

    public Dynarray(int size) {
        data = new int[size];
    }

    public int Length() {
        return data.Length;
    }

    public int Get(int index) {
        return data[index];
    }

    public void Set(int index, int value) {
        data[index] = value;
    }

    public void Insert(int index, int value) {
        int[] newData = new int[data.Length + 1];
        for (int i=0;  i<index; i++) {
            newData[i] = data[i];
        }
        newData[index] = value;
        for (int i=index+1; i<newData.Length; i++) {
            newData[i] = data[i - 1];
        }
        data = newData;
    }

    public void Add(int value) {
        Insert(Length(), value);
    }

    public void Remove(int index) {
        int[] newData = new int[data.Length - 1];
        for (int i = 0; i < data.Length - 1; i++) {
            newData[i] = data[i + (i >= index ? 1 : 0)];
        }
        data = newData;
    }

    public int[] DebugGetRawData() {
        return data;
    }
}
