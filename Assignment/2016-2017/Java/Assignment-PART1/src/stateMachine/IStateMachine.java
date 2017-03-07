package stateMachine;


import component.Updateable;

public interface IStateMachine extends Updateable {

    boolean isBusy();

    void reset();
}
