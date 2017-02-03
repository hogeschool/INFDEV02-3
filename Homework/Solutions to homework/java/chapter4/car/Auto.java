package chapter4.car;

import java.time.Duration;
import java.util.List;

public class Auto implements Entity{
    Position positionX;
    FuelTank fuelTank;
    Engine engine;
    Gearbox gearbox;
    List<Wheel> wheel;

    public Auto(Position positionX, FuelTank fuelTank, Engine engine, Gearbox gearbox, List<Wheel> wheel) {
        this.positionX = positionX;
        this.fuelTank = fuelTank;
        this.engine = engine;
        this.gearbox = gearbox;
        this.wheel = wheel;
    }

    @Override
    public Position getPositionX() {
        return positionX;
    }

    public void drive(Duration duration){
        if(fuelTank.getFuelAmount() > 0f){
            float litersOfFuel  = fuelTank.pumpFuel(duration);
            float rpm_e = engine.burn(litersOfFuel);
            float rpm_g = gearbox.turn(rpm_e);
            float distance = wheel.get(0).turn(rpm_g);

            positionX.add(new Position(distance));

            System.out.println("vroem");
        }else{
            System.out.println("No fuel to drive, you better walk.");
        }
    }

    public void tick(Duration duration){
        drive(duration);
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
