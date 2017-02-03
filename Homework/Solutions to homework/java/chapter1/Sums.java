package chapter1;

import java.util.Scanner;

public class Sums {
    public static void main(String[] args)  {
        System.out.print("Enter a small number: ");

        Scanner scanner = new Scanner(System.in);
        int number = scanner.nextInt();

        int sum = 0;

        for(int i=0; i < number; i++){
            sum += i;
        }

        System.out.println(sum);
    }
}
