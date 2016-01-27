package exercises.exercise2.vehicles;

import exercises.exercise2.fuels.Diesel;
import exercises.exercise2.fuels.Fuel;

public class Truck implements Vehicle{
    Fuel tank = new Diesel(0);

    public boolean loadFuel(Fuel fuel){
        if(! (fuel instanceof Diesel)){
            return false;
        }
        tank = fuel;
        return true;
    }

    public boolean move(){
        if(tank.getAmount() > 0){
            tank.setAmount(tank.getAmount() - 1);
            return true;
        }

        return false;
    }

    @Override
    public String toString() {
        final StringBuilder sb = new StringBuilder("Truck{");
        sb.append("tank=").append(tank);
        sb.append('}');
        return sb.toString();
    }
}
