package assignment4;

public class Auto extends Entity{
    private float positionX;

    FuelTank fuelTank;
    Engine engine;
    Gearbox gearbox;
    Wheels wheels;

    public Auto(float positionX, FuelTank fuelTank, Engine engine, Gearbox gearbox, Wheels wheels) {
        this.positionX = positionX;
        this.fuelTank = fuelTank;
        this.engine = engine;
        this.gearbox = gearbox;
        this.wheels = wheels;
    }

    @Override
    public float getPositionX() {
        return positionX;
    }

    public void setPositionX(float positionX) {
        this.positionX = positionX;
    }

    public void drive(float timeDifference){
        if(fuelTank.getFuelAmount() > 0f){
            float fuel  = fuelTank.pumpFuel(timeDifference);
            float rpm_e = engine.turn(fuel);
            float rpm_g = gearbox.turn(rpm_e);
            float distance = wheels.turn(rpm_g);

            positionX += distance;

            System.out.println("vroem");
        }else{
            System.out.println("No fuel to drive, you better walk.");
        }
    }

    public void tick(float timeDifference){
        drive(timeDifference);
    }

    public void loadFuel(float fuelAmount){
        fuelTank.addFuel(fuelAmount);
    }

    @Override
    public String toString() {
        return "An auto{" +
                "positionX=" + positionX +
                ", fuelTank=" + fuelTank +
                '}';
    }
}
