package chapter2.statemachine;

public interface IStateMachine {
    void update(float deltaTime);
    boolean isDone();
}
