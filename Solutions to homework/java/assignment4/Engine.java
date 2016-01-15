package assignment4;

public abstract class Engine implements PowertrainComponent {
    int cylinders;
    int horsePower;

    int getCylinders(){
        return cylinders;
    }

    int getHorsePower(){
        return horsePower;
    }
}
