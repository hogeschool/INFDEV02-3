package chapter2.fuels_advanced;

public abstract class AbstractFuel implements IFuel {
    private int amount;

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }
}
