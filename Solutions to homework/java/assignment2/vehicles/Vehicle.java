package assignment2.vehicles;

import assignment2.fuels.Fuel;
import assignment2.fuels.Gasoline;

public interface Vehicle {
    Fuel tank = new Gasoline(0);

    public boolean loadFuel(Fuel fuel);
    public boolean move();
}
