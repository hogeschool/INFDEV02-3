package assignment4;

public class Mercedes722Comma6Gearbox extends Gearbox {

    public float turn(float engineRPM){
        return 7.5f * engineRPM / (3500f * 18 * 2.54f);
    }
}
