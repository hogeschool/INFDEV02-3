package container;

import component.Drawable;
import javafx.geometry.Point2D;

public interface IContainer extends Drawable {
    int getCurrentAmount();

    void setCurrentAmount(int currentAmount);

    int getMaxCapacity();

    boolean addContent(int amount);

    Point2D getPosition();

    void setPosition(Point2D position);
}

