package assignment4;

public abstract class FuelTank implements Component{
    float capacity;
    float fuelAmount;

    abstract float pumpFuel(float timeDifference);

    float getCapacity(){
        return capacity;
    }

    float getFuelAmount(){
        return fuelAmount;
    }

    void addFuel(float amount){
        fuelAmount += amount;

        float difference = fuelAmount - capacity;
        if(difference > 0){
            fuelAmount = capacity;
            System.out.printf("You spilled %f liters of precious fuel :( \n", difference);
        }
    }

    @Override
    public String toString() {
        return "FuelTank{" +
                "fuelAmount=" + fuelAmount +
                '}';
    }
}
