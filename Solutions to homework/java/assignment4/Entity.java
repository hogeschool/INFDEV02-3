package assignment4;

public abstract class Entity {
    float positionX = 0f;

    abstract void tick(float timeDifference);

    float getPositionX(){
        return positionX;
    }
}
