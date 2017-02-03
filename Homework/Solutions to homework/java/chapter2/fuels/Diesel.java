package chapter2.fuels;

public class Diesel implements IFuel{
    int amount;

    public Diesel(int amount) {
        setAmount(amount);
    }

    @Override
    public int getAmount() {
        return amount;
    }

    @Override
    public void setAmount(int amount) {
        this.amount = amount;
    }

}