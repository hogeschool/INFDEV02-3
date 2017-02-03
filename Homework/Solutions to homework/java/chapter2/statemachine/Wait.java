package chapter2.statemachine;

public class Wait implements IStateMachine {
    float waitingTime;
    boolean isDone = false;

    public Wait(float waitingTime) {
        this.waitingTime = waitingTime;
    }

    @Override
    public void update(float deltaTime) {
        waitingTime -= deltaTime;

        if (waitingTime <= 0){
            isDone = true;
        }
    }

    @Override
    public boolean isDone() {
        return isDone;
    }
}
