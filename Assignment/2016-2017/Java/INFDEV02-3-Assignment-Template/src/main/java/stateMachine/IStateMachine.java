package stateMachine;


import component.IUpdateable;

public interface IStateMachine extends IUpdateable {

    boolean isBusy();

    void reset();
}
