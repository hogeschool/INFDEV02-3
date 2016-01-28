package drills.drill1;

public class Car {
    //attributes
    int positionX;
    int positionY;
    String name;

    //constructor
    public Car(int positionX, int positionY, String name) {
        this.positionX = positionX;
        this.positionY = positionY;
        this.name = name;
    }

    //a method
    public void drive(){
        this.positionX += 1;
        System.out.println(this);
    }

    @Override
    public String toString() {
        return "Car{" +
                "positionX=" + positionX +
                ", positionY=" + positionY +
                ", name='" + name + '\'' +
                '}';
    }

    //the main method, this is the start of the application
    public static void main(String[] args) {
        Car herbie = new Car(0,0,"Herbie");
        herbie.drive();
        herbie.drive();
        herbie.drive();
    }
}
