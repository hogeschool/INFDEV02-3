package chapter2.fuels;

public class Dilithium implements IFuel {
    int amount;

    public Dilithium(int amount) {
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
