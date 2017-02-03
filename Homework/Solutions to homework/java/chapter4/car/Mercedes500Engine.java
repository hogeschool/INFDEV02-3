package chapter4.car;

public class Mercedes500Engine implements Engine{

    @Override
    public int getCylinders() {
        return 12;
    }

    @Override
    public int getHorsePower() {
        return 517;
    }

    @Override
    public float burn(float litersOfFuel){
        return turn(litersOfFuel);
    }

    @Override
    public float turn(float fuel){
        return 3500 * fuel;
    }
}