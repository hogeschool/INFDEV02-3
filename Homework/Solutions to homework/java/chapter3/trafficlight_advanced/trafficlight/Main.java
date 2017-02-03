package chapter3.trafficlight_advanced.trafficlight;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {
        TrafficLight t = new TrafficLight();
        while (true) {
            System.out.println(t);
            try {
                Thread.sleep(1000);
                t.Tick(1.0f);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
