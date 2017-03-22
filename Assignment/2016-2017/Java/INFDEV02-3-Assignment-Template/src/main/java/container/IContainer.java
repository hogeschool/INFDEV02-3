package container;

import component.IDrawable;
import javafx.geometry.Point2D;

public interface IContainer extends IDrawable {
    int getCurrentAmount();

    void setCurrentAmount(int currentAmount);

    int getMaxCapacity();

    boolean addContent(int amount);

    Point2D getPosition();

    void setPosition(Point2D position);
}

