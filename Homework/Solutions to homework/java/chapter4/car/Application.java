package chapter4.car;

import java.io.IOException;
import java.time.Duration;
import java.time.temporal.ChronoUnit;
import java.util.Arrays;

public class Application {
    public static void main(String[] args) {
        Wheel18Inch[] wheels = {new Wheel18Inch(),new Wheel18Inch(),new Wheel18Inch(),new Wheel18Inch()};

        Auto auto = new Auto(
                new Position(0f),
                new LargeTank(),
                new Mercedes500Engine(),
                new Mercedes722Comma6Gearbox(),
                Arrays.asList(wheels)
        );

        auto.loadFuel(20f);

        while(true){
            auto.drive(Duration.of(1L, ChronoUnit.MINUTES));
            System.out.println(auto);
            try {
                System.out.println("press enter to drive");
                System.in.read();
            }catch (IOException ioe){
                System.err.println(ioe.getMessage());
            }
        }
    }
}
