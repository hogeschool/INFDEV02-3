package chapter3.trafficlight_advanced.trafficlight;

public class TrafficLight {
    Color color;
    Timer toNextLight;

    public TrafficLight() {
        color = Color.Green;
        toNextLight = new Timer(this::FromGreenToYellow, 6.0f);
    }

    public void Tick(float deltaTime) {
        toNextLight.Tick(deltaTime);
    }

    @Override
    public String toString() {
        return "TrafficLight{" +
                "color=" + color +
                ", toNextLight=" + toNextLight +
                '}';
    }

    public void FromGreenToYellow(){
        color = Color.Yellow;
        toNextLight = new Timer(this::FromYellowToRed, 6.0f);
    }

    public void FromYellowToRed(){
        color = Color.Red;
        toNextLight = new Timer(this::FromRedToGreen, 6.0f);
    }

    public void FromRedToGreen(){
        color = Color.Green;
        toNextLight = new Timer(this::FromGreenToYellow, 6.0f);
    }
}

