package assignment4;

import java.io.IOException;

public class Application {
    public static void main(String[] args) {

        Auto auto = new Auto(
                0f,
                new LargeTank(),
                new Mercedes500Engine(),
                new Mercedes722Comma6Gearbox(),
                new Wheel18Inch()
        );

        auto.loadFuel(2f);

        while(true){
            auto.tick(1f);
            System.out.println(auto);
            try {
                System.in.read();
            }catch (IOException ioe){
                System.err.println(ioe.getMessage());
            }
        }

    }
}
