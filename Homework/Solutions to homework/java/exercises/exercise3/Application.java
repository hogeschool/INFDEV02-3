package exercises.exercise3;

import java.io.IOException;

public class Application {

    public static void main(String[] args) {
        TrafficLight t = new TrafficLight();
        while (true){
            t.tick(1.0f);
            System.out.println(t);

            try {
                System.in.read();
            }catch(IOException ioe){
                System.err.println(ioe);
            }
        }
    }
}
