package truck;

import component.IComponent;
import container.IContainer;
import javafx.geometry.Point2D;

public interface ITruck extends IComponent {
    void startEngine();

    void addContainer(IContainer container);

    IContainer getContainer();

    Point2D getPosition();

    Point2D getVelocity();
}
