package exercises.exercise3;

public class Timer{
    private Event onReached;
    private float targetTime;
    private float elapsedTime;

    public Timer(Event onReached, float targetTime){
        this.elapsedTime = 0.0f;
        this.onReached = onReached;
        this.targetTime = targetTime;
    }

    public void tick(float deltaTime){
        elapsedTime += deltaTime;
        if (elapsedTime >= targetTime) {
            onReached.perform();
        }
    }

    @Override
    public String toString()
    {
        return elapsedTime + "/" + targetTime;
    }

}
