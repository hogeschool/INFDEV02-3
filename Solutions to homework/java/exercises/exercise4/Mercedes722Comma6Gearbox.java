package exercises.exercise4;

import java.util.Arrays;
import java.util.List;

public class Mercedes722Comma6Gearbox implements Gearbox {

    List<Integer> gears;
    int currentGearIndex;

    public Mercedes722Comma6Gearbox() {
        gears = Arrays.asList(300, 200, 100, 50, 10);
        currentGearIndex = 1;
    }

    @Override
    public float turn(float engineRPM){
        return 7.5f * engineRPM / (3500f * gears.get(currentGearIndex) * 2.54f);
    }

    public void shiftTo(int gear) {
        currentGearIndex = gear;
    }
}
