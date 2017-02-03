package drills.drill1;

import java.util.Scanner;

/**
 * Make a function that sums all numbers between two
 * inputs read from the console and prints the result
 */
public class Sum {
    public static void main(String[] args)  {
        Scanner scanner = new Scanner(System.in);

        System.out.print("Enter a starting number: ");
        int start = scanner.nextInt();

        System.out.print("Enter an ending number: ");
        int end = scanner.nextInt();


        int sum = 0;
        for(int i=start; i < end; i++){
            sum += i;
        }

        System.out.println(sum);
    }
}
