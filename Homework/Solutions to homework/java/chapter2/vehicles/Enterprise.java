package chapter2.vehicles;

import chapter2.fuels.Dilithium;
import chapter2.fuels.IFuel;

public class Enterprise implements Vehicle {
    IFuel tank = new Dilithium(0);

    public boolean loadFuel(IFuel fuel){
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
