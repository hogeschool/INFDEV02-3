package stateMachine;

public class Timer extends AbstractStateMachine {
    float initialTime;
    float time;

    public Timer(float time) {
        this.time = time;
        this.initialTime = time;
        this.isBusy = true;
    }

    public void reset() {
        time = initialTime;
        setBusy(true);
    }

    public void update(float dt) {
        time -= dt;
        if(time <= 0){
            setBusy(false);
        }
    }
}

