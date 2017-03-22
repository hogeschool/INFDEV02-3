package game;

import javafx.animation.AnimationTimer;
import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.stage.Stage;

public class Game extends Application{
    GameState gameState;

    protected void loadContent(){
        
        Image background = new Image(Game.class.getResource("/images/background.png").toString());
        Image mine_cart = new Image(Game.class.getResource("/images/mine_cart.png").toString());
        Image product_box = new Image(Game.class.getResource("/images/product_box.png").toString());
        Image volvo = new Image(Game.class.getResource("/images/volvo.png").toString());
        Image mine = new Image(Game.class.getResource("/images/mine.png").toString());
        Image ikea = new Image(Game.class.getResource("/images/ikea.png").toString());
        Image ore_container = new Image(Game.class.getResource("/images/ore_container.png").toString());
        Image product_container = new Image(Game.class.getResource("/images/product_container.png").toString());

        gameState = new GameState(background, mine_cart, product_box, volvo, mine, ikea, ore_container, product_container);
    }

    @Override
    public void start(Stage stage){
        stage.setTitle("INFDEV02-3 - Assignment");

        Group root = new Group();
        Scene theScene = new Scene( root );
        stage.setScene( theScene );

        Canvas canvas = new Canvas( 800, 800 );
        root.getChildren().add( canvas );

        GraphicsContext gc = canvas.getGraphicsContext2D();

        final long startNanoTime = System.nanoTime();

        loadContent();
        stage.show();
        //this acts as the main loop
        new AnimationTimer(){
            @Override
            public void handle(long currentNanoTime){
                //clear the canvas before painting over it
                gc.clearRect(0,0,canvas.getWidth(),canvas.getHeight());
                gameState.draw(gc);
                gameState.update(0.3f);
            }
        }.start();
    }

    public static void main(String[] args) {
        launch();
    }
}
