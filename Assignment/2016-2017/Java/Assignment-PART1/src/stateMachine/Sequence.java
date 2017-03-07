package stateMachine;

public class Sequence extends AbstractStateMachine {
    IStateMachine current, next;
    IStateMachine s1, s2;

    public Sequence(IStateMachine s1, IStateMachine s2) {
        current = s1;
        next    = s2;
        this.s1 = s1;
        this.s2 = s2;
    }

    @Override
    public void reset() {
        s1.reset();
        s2.reset();
        current = s1;
        setBusy(true);
    }

    @Override
    public void update(float dt) {
        if(isBusy){
            current.update(dt);
            if(!current.isBusy()){
                current = next;
            }
            if (!next.isBusy()){
                setBusy(false);
            }
        }
    }
}
