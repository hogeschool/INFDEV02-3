package chapter1;

public class Interval {
    int start;
    int end;

    public Interval(int start, int end) {
        this.start = start;
        this.end = end;
    }

    public int sum(){
        int result = 0;
        for (int i = this.start; i < this.end; i++) {
            result += i;
        }
        return result;
    }

    public long product(){
        long result = 1;

        for (int i = this.start; i < this.end; i++) {
            result *= i;
        }
        return result;
    }

    public static void main(String[] args) {
        Interval interval = new Interval(1,5);
        System.out.printf("The result should be %d and is: %d\n", 10, interval.sum());
        System.out.printf("The result should be %d and is: %d\n", 24, interval.product());
    }
}
