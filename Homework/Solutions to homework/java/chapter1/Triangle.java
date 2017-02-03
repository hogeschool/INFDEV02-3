package chapter1;

public class Triangle {
    public static void main(String[] args) {
        String result = "";
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < i; j++) {
                result += "*";
            }
            result += "\n";
        }
        System.out.println(result);
    }
}
