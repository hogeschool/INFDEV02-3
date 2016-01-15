package assignment4;

public class LargeTank extends FuelTank{

    public LargeTank() {
        this.capacity = 125;
    }

    public float pumpFuel(float timeDifference){
        float amount = timeDifference;

        if(this.fuelAmount >= amount){
            this.fuelAmount -= amount;
            return amount;
        }

        return 0f;
    }
}
