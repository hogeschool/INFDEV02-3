package Game;

import component.IComponent;
import factory.IFactory;
import stateMachine.IAction;
import stateMachine.IStateMachine;
import truck.ITruck;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class GameState implements IComponent {
    List<ITruck> trucks;
    IFactory factory1;
    IFactory factory2;
    List<IStateMachine> processes;

    Image background;

    public GameState(Image background, Image mineCart, Image productBox, Image volvo, Image mine, Image ikea, Image oreContainer, Image productContainer) {
        this.background = background;

        factory1 = null; //new MiningFactory(new Point2D(100, 70), new Dimension2D(150, 230/2), new Point2D(100,0), new Point2D(5,0), mine, oreContainer, mineCart, volvo);
        factory2 = null; //new IkeaFactory(new Point2D(600, 340), new Dimension2D(150, 175/2), new Point2D(-100,0), new Point2D(-4,0), ikea, productContainer, productBox, volvo);

        trucks = new ArrayList<>();

        this.processes = new ArrayList<>();
//        this.processes.add(new Repeat(new Call(new AddTruckFromFactory(factory1, trucks))));
//        this.processes.add(new Repeat(new Call(new AddTruckFromFactory(factory2, trucks))));
    }

    class AddTruckFromFactory implements IAction {
        IFactory factory;
        List<ITruck> trucks;

        public AddTruckFromFactory(IFactory factory, List<ITruck> trucks) {
            this.factory = factory;
            this.trucks = trucks;
        }

        public void run(){
            ITruck truck = factory.getReadyTruck();
            if (truck != null){
                truck.startEngine();
                trucks.add(truck);
            }
        }
    }
    
    public void update(float dt){
        trucks.removeIf(truck -> truck.getPosition().getX() < -50 || truck.getPosition().getX() > 1000);

        for (IStateMachine process : processes) {
            process.update(dt);
        }

        for (ITruck truck : trucks) {
            truck.update(dt);
        }

//        factory1.update(dt);
//        factory2.update(dt);
    }

    public void draw(GraphicsContext gc){
        gc.drawImage(background, 0, 0 );

        for (ITruck truck : trucks) {
            truck.draw(gc);
        }
        LocalDateTime l;
//        factory1.draw(gc);
//        factory2.draw(gc);
    }
}