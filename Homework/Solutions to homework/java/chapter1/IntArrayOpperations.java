package chapter1;

public class IntArrayOpperations {
    int[] array;

    public IntArrayOpperations(int[] array) {
        this.array = array;
    }

    public int sum(){
        int result = 0;

        for (int i = 0; i < array.length; i++) {
            result += array[i];
        }
        return result;
    }

    public long product(){
        long result = 1;

        for (int i = 0; i < array.length; i++) {
            result *= array[i];
        }
        return result;
    }

    public static void main(String[] args) {
        IntArrayOpperations intArrayOpperations = new IntArrayOpperations(new int[]{1,19,7,6,5});
        System.out.printf("The result should be %d and is: %d\n", 10, intArrayOpperations.sum());
        System.out.printf("The result should be %d and is: %d\n", 24, intArrayOpperations.product());
    }
}
