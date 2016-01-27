package exercises.exercise4;

public class Wheel18Inch implements Wheel {
    float radius;

    public Wheel18Inch() {
        this.setRadius(18f);
    }

    @Override
    public float getRadius() {
        return radius;
    }

    @Override
    public void setRadius(float radius) {
        this.radius = radius;
    }

    @Override
    public float getRadiusInCentimeters() {
        return getRadius() * CENTIMETERS_PER_INCH;
    }

    @Override
    public float turn(float gearboxRPM){
        return gearboxRPM * getRadiusInCentimeters();
    }

}
