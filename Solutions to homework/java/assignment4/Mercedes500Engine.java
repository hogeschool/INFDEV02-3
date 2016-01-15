package assignment4;

public class Mercedes500Engine extends Engine{

    public Mercedes500Engine() {
        cylinders = 12;
        horsePower = 517;
    }

    public float turn(float fuel){
        return 3500 * fuel;
    }
}