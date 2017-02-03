package chapter4.car;

public interface Engine extends PowerTrainComponent {
    int getCylinders();
    int getHorsePower();

    float burn(float litersOfFuel);
}
