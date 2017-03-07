package stateMachine;

public abstract class AbstractStateMachine implements IStateMachine {
    boolean isBusy = true;

    public boolean isBusy(){
        return isBusy;
    }

    public void setBusy(boolean busy) {
        isBusy = busy;
    }

}
