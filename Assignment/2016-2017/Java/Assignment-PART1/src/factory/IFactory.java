package factory;

import component.IComponent;
import container.IContainer;
import truck.ITruck;
import javafx.geometry.Point2D;

import java.util.List;

public interface IFactory extends IComponent {
    ITruck getReadyTruck();

    Point2D getPosition();

    List<IContainer> getProductsToShip();
}
