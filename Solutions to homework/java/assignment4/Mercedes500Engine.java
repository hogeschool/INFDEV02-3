package assignment4;

public class Mercedes500Engine extends Engine{

    public Mercedes500Engine() {
        this.cylinders = 12;
        this.horsePower = 517;
    }

    public float burn(float litersOfFuel){
        return turn(litersOfFuel);
    }

    public float turn(float fuel){
        return 3500 * fuel;
    }
}