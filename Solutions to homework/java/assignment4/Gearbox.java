package assignment4;

import java.util.List;

public abstract class Gearbox implements PowerTrainComponent {
    List<Integer> gears;
    int currentGearIndex;

    void shiftTo(int gear){
        currentGearIndex = gear;
    }

    public float turn(float engineRPM){
        return 7.5f * engineRPM / (3500f * gears.get(currentGearIndex) * 2.54f);
    }
}
