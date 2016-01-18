package assignment4;

public abstract class Wheel implements PowerTrainComponent {
    private float radius;

    public static final float CENTIMETERS_PER_INCH = 2.54f;

    float getRadius(){
        return radius;
    }

    public void setRadius(float radius) {
        this.radius = radius;
    }

    public float getRadiusInCentimeters(){
        return getRadius() * CENTIMETERS_PER_INCH;
    }

    public float turn(float gearboxRPM){
        return gearboxRPM * getRadiusInCentimeters();
    }
}
