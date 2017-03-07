package stateMachine;

public class Print extends AbstractStateMachine {
    String message;

    public Print(String message) {
        this.message = message;
    }

    public void reset() {
        setBusy(true);
    }

    public void update(float dt) {
        System.out.println(message);
        setBusy(false);
    }
}

