package exercises.exercise2.vehicles;

import exercises.exercise2.fuels.Fuel;
import exercises.exercise2.fuels.Gasoline;

public class Car implements Vehicle{
    Fuel tank = new Gasoline(0);

    public boolean loadFuel(Fuel fuel){
        if(! (fuel instanceof Gasoline)){
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
        final StringBuilder sb = new StringBuilder("Car{");
        sb.append("tank=").append(tank);
        sb.append('}');
        return sb.toString();
    }

}
