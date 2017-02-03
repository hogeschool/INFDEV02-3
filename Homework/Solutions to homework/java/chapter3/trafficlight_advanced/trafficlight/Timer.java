package chapter3.trafficlight_advanced.trafficlight;

public class Timer {
    private Event onReached;
    private float targetTime;
    private float elapsedTime;

    public Timer(Event onReached, float targetTime) {
        elapsedTime = 0.0f;
        this.onReached = onReached;
        this.targetTime = targetTime;
    }

    public void Tick(float deltaTime) {
        elapsedTime += deltaTime;
        if (elapsedTime >= targetTime) {
            onReached.perform();
        }
    }

    @Override
    public String toString() {
        return "Timer{" +
                "targetTime=" + targetTime +
                ", elapsedTime=" + elapsedTime +
                '}';
    }
}