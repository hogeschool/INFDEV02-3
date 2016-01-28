package exercises.exercise2.vehicles;

import exercises.exercise2.fuels.Fuel;
import exercises.exercise2.fuels.Gasoline;

public interface Vehicle {
    Fuel tank = new Gasoline(0);

    public boolean loadFuel(Fuel fuel);
    public boolean move();
}
