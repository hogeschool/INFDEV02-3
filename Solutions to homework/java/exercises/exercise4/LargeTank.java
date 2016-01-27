package exercises.exercise4;

import java.time.Duration;

public class LargeTank implements FuelTank{
    private float fuelCapacity;
    private float fuelAmount;
    private float pumpSpeed;

    public LargeTank() {
        this.fuelCapacity = 125;
        this.pumpSpeed = 1f;
    }

    @Override
    public float getFuelAmount(){
        return fuelAmount;
    }

    @Override
    public void addFuel(float amount){
        fuelAmount += amount;

        float difference = fuelAmount - fuelCapacity;
        if(difference > 0){
            fuelAmount = fuelCapacity;
            System.out.printf("You spilled %f liters of precious fuel :( \n", difference);
        }
    }

    @Override
    public float pumpFuel(Duration duration){
        float amount = duration.toMinutes() * pumpSpeed ;

        if(this.fuelAmount >= amount){
            this.fuelAmount -= amount;
            return amount;
        }
        return 0f;
    }

    @Override
    public String toString() {
        return "FuelTank{" +
                "fuelAmount=" + fuelAmount +
                '}';
    }
}