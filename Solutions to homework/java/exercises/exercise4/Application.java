package exercises.exercise4;

import com.sun.tools.javac.util.List;

import java.io.IOException;
import java.time.Duration;
import java.time.temporal.ChronoUnit;

public class Application {
    public static void main(String[] args) {
        Auto auto = new Auto(
                new Position(0f),
                new LargeTank(),
                new Mercedes500Engine(),
                new Mercedes722Comma6Gearbox(),
                List.of(new Wheel18Inch(),new Wheel18Inch(),new Wheel18Inch(),new Wheel18Inch())
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
