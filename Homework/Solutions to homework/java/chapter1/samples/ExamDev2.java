package chapter1.samples;

public class ExamDev2 {
    public static void main(String[] args) {
        ExamDev2 a = new ExamDev2();
        q4();
    }

    public static void q4() {
        int[] list = {1, 2, 3, 4};
        int i = 0;
        int sum = 0;

        for (int x: list) {
            if (i % 2 == 0) {
                sum += x;
            }

            i++;
        }

        System.out.println(sum);

    }

}
