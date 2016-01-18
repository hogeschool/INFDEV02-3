package assignment4;

public class Distance {

    private float distance;

    public Distance(float distance) {
        this.distance = distance;
    }


    public float getDistance() {
        return distance;
    }

    public void setDistance(float distance) {
        this.distance = distance;
    }

    public Position add(Position that){
        return new Position(this.getDistance() + that.getPosition());
    }
}
