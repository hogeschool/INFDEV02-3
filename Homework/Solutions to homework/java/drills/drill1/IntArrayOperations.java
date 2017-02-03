package drills.drill1;

public class IntArrayOperations {


    public static void main(String[] args) {
        int [] nums = {12,5,16,3,9,56,100};

        System.out.println("The sum is: "+ checkAnswer(sum(nums),201));
        System.out.println("The product is: " +  checkAnswer(product(nums), 145152000));
        System.out.println("" +  checkAnswer(average(nums), 28.714285f));
    }


    /**
     * calculate the sum of an array of integers using a for-loop
     * @param nums an array of integers
     * @return the sum
     */
    public static  int sum(int[] nums){
        int sum = 0;
        for (int i = 0; i < nums.length; i++) {
            sum += nums[i];
        }
        return sum;
    }

    /**
     * calculate the product of an array of integers using a for-loop
     * @param nums an array of integers
     * @return the product
     */
    public static int product(int[] nums){
        int  product = 1;
        for (int i = 0; i < nums.length; i++) {
            product *= nums[i];
        }
        return product;
    }

    /**
     * calculate the average of an array of integers.
     * Now with reusing one of the functions you wrote before.
     * @param nums an array of integers
     * @return the average
     */
    public static float average(int[] nums){
        return (float)sum(nums)/nums.length;
    }



    public static String checkAnswer(int expected, int actual){
        return expected == actual ? "correct!":"wrong";
    }

    public static String checkAnswer(float expected, float actual){
        return Math.abs(expected-actual) < 0.001f ? "correct!":"wrong";
    }
}
