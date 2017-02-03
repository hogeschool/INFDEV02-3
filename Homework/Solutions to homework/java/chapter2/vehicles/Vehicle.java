package chapter2.vehicles;


import chapter2.fuels.Gasoline;
import chapter2.fuels.IFuel;

public interface Vehicle {
    IFuel tank = new Gasoline(0);

    public boolean loadFuel(IFuel fuel);
    public boolean move();
}
