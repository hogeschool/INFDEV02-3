package assignment4;

public abstract class Wheels implements PowertrainComponent{
    float radius;

    public static final float CENTIMETERS_IN_AN_INCH = 2.54f;

    float getRadius(){
        return radius;
    }

    public float getRadiusInCentimeters(){
        return getRadius() * CENTIMETERS_IN_AN_INCH;
    }

    public float turn(float gearboxRPM){
        return gearboxRPM * getRadiusInCentimeters();
    }
}
