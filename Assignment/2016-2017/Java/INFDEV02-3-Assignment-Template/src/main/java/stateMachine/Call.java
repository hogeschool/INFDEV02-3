package stateMachine;

public class Call extends AbstractStateMachine {
    IAction action;

    public Call(IAction action) {
        this.action = action;
    }

    @Override
    public void reset() {
        setBusy(true);
    }

    @Override
    public void update(float dt) {
        action.run();
        setBusy(false);
    }
}
