package exercises.exercise4;

public class Position {
    private float position;

    public Position(float position) {
        this.position = position;
    }


    public float getPosition() {
        return position;
    }

    public void setPosition(float position) {
        this.position = position;
    }

    public void add(Position that){
        setPosition(this.getPosition() + that.getPosition());
    }


    @Override
    public String toString() {
        return "Position{" +
                "(" + position +
                ")}";
    }
}
