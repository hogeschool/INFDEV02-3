package exercises.exercise2.vehicles;

import exercises.exercise2.fuels.Dilithium;
import exercises.exercise2.fuels.Fuel;

public class Enterprise implements Vehicle {
    Fuel tank = new Dilithium(0);

    public boolean loadFuel(Fuel fuel){
        if(! (fuel instanceof Dilithium)){
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
        final StringBuilder sb = new StringBuilder("Enterprise{");
        sb.append("tank=").append(tank);
        sb.append('}');
        return sb.toString();
    }
}
