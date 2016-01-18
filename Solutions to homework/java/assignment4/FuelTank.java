package assignment4;


import java.time.Duration;

public class FuelTank implements Component{
    float fuelCapacity;
    float fuelAmount;
    float pumpSpeed;

    float getFuelCapacity(){
        return fuelCapacity;
    }

    public void setFuelCapacity(float fuelCapacity) {
        this.fuelCapacity = fuelCapacity;
    }

    float getFuelAmount(){
        return fuelAmount;
    }

    public void setFuelAmount(float fuelAmount) {
        this.fuelAmount = fuelAmount;
    }

    void addFuel(float amount){
        fuelAmount += amount;

        float difference = fuelAmount - fuelCapacity;
        if(difference > 0){
            fuelAmount = fuelCapacity;
            System.out.printf("You spilled %f liters of precious fuel :( \n", difference);
        }
    }

    @Override
    public String toString() {

        return "FuelTank{" +
                "fuelAmount=" + fuelAmount +
                '}';
    }

    public float pumpFuel(Duration duration){
        float amount = duration.toMinutes() * pumpSpeed ;

        if(this.fuelAmount >= amount){
            this.fuelAmount -= amount;
            return amount;
        }

        return 0f;
    }

}
