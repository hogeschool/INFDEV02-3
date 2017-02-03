package exercises.exercise4;

public interface Engine extends PowerTrainComponent {
    int getCylinders();
    int getHorsePower();

    float burn(float litersOfFuel);
}
