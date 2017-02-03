package chapter2.applied;

import chapter1.Counter;
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

/**
 * Watch this video for an introduction to
 * making a GUI in java https://www.youtube.com/watch?v=FLkOX4Eez6o
 *
 * We built a GUI around the Counter from chapter 1.
 */
public class Main extends Application {
    Button tickButton;
    Label counterLabel;
    Counter counter;

    public static void main(String[] args) {
        Main.launch();
    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        primaryStage.setTitle("Counting app");
        tickButton = new Button("Tick");
        counterLabel = new Label();
        counter = new Counter();
        counter.onTarget(10, () -> {
            counter.reset();
            System.out.println("Target reached, reset to: " + counter.count);
        });

        counterLabel.setText("" + counter.count);
        tickButton.setOnAction(event -> {
            counter.tick();
            System.out.println(counter);
            counterLabel.setText("" + counter.count);
        });

        VBox layout = new VBox(20);
        layout.getChildren().addAll(tickButton, counterLabel);
        Scene scene = new Scene(layout, 300, 250);
        primaryStage.setScene(scene);
        primaryStage.show();
    }
}
