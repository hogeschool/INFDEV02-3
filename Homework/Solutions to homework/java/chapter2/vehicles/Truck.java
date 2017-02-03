package chapter2.vehicles;

import chapter2.fuels.Diesel;
import chapter2.fuels.IFuel;

public class Truck implements Vehicle{
    IFuel tank = new Diesel(0);

    public boolean loadFuel(IFuel fuel){
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
