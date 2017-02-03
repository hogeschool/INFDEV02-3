package chapter2.statemachine;

public class Sequence implements IStateMachine {
    IStateMachine s1;
    IStateMachine s2;

    public Sequence(IStateMachine s1, IStateMachine s2) {
        this.s1 = s1;
        this.s2 = s2;
    }

    @Override
    public void update(float deltaTime) {
        if (!isDone()) {
            if (!s1.isDone()) {
                s1.update(deltaTime);
            } else {
                s2.update(deltaTime);
            }
        }
    }

    @Override
    public boolean isDone() {
        return s1.isDone() && s2.isDone();
    }
}
