package assignment4;

import java.time.Duration;

public abstract class Entity {
    Position positionX = new Position(0f);

    abstract void tick(Duration duration);

    Position getPositionX(){
        return positionX;
    }
}
