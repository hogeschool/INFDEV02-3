package chapter2.statemachine;

public class Print implements IStateMachine{
    String message;
    boolean isDone;

    public Print(String message) {
        this.message = message;
        isDone = false;
    }

    @Override
    public void update(float deltaTime) {
        System.out.println(message);
        isDone = true;
    }

    @Override
    public boolean isDone() {
        return isDone;
    }
}
