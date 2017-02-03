package chapter4.car;

public interface Wheel extends PowerTrainComponent {
    float CENTIMETERS_PER_INCH = 2.54f;

    float getRadius();

    void setRadius(float radius);

    float getRadiusInCentimeters();

    float turn(float gearboxRPM);
}
