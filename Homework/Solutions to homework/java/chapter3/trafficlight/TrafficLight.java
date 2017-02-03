package chapter3.trafficlight;

public class TrafficLight {
    Color color;
    Timer toNextLight;

    public TrafficLight() {
        color = Color.Green;
        toNextLight = new Timer(new FromGreenToYellow(this), 6.0f);
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

    //-------------------------------------
    //Inner classes for the state changes
    class FromGreenToYellow implements Event {
        TrafficLight context;

        public FromGreenToYellow(TrafficLight ctxt) {
            context = ctxt;
        }

        public void perform() {
            context.color = Color.Yellow;
            context.toNextLight = new Timer(new FromYellowToRed(context), 3.0f);
        }
    }

    class FromYellowToRed implements Event {
        TrafficLight context;

        public FromYellowToRed(TrafficLight ctxt) {
            context = ctxt;
        }

        public void perform() {
            context.color = Color.Red;
            context.toNextLight = new Timer(new FromRedToGreen(context), 10.0f);
        }
    }

    class FromRedToGreen implements Event {
        TrafficLight context;

        public FromRedToGreen(TrafficLight ctxt) {
            context = ctxt;
        }

        public void perform() {
            context.color = Color.Green;
            context.toNextLight = new Timer(new FromGreenToYellow(context), 6);
        }
    }
}

interface Event {
    void perform();
}


