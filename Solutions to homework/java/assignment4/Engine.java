package assignment4;

public abstract class Engine implements PowerTrainComponent {
    int cylinders;
    int horsePower;

    int getCylinders(){
        return cylinders;
    }

    int getHorsePower(){
        return horsePower;
    }

    float burn(float litersOfFuel){
        return turn(litersOfFuel);
    };
}
