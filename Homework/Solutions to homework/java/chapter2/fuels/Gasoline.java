package chapter2.fuels;

public class Gasoline implements IFuel {
    int amount;

    public Gasoline(int amount) {
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
