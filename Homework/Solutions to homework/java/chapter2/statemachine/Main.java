package chapter2.statemachine;

import javafx.animation.AnimationTimer;
import javafx.application.Application;
import javafx.stage.Stage;

public class Main extends Application{
    public static void main(String[] args) {
        launch();
    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        System.out.println("Started");

        Sequence s = new Sequence(new Wait(10f), new Print("Hello World!"));
        LongValue lastNanoTime = new LongValue( System.nanoTime() );

        new AnimationTimer() {
            public void handle(long currentNanoTime) {

                // calculate time since last update.
                double elapsedTime = (currentNanoTime - lastNanoTime.value) / 1000000000.0;
                lastNanoTime.value = currentNanoTime;
                s.update((float) elapsedTime);
            }
        }.start();
    }
}

class LongValue {
    public long value;

    public LongValue(long i){
        value = i;
    }
}