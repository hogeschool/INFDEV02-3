package chapter1;

public class Counter {
    public int count;
    int target;
    Runnable onTarget;

    public Counter() {
        this.count = 0;
        this.target = -1;
    }

    public Counter(int startFrom) {
        this.count = startFrom;
        this.target = -1;
    }

    public void tick(){
        count++;

        if(count == target){
            onTarget.run();
        }
    }

    public void reset(){
        count = 0;
    }

    public static Counter plus(Counter c1, Counter c2){
        return new Counter(c1.count + c2.count);
    }


    public void onTarget(int target, Runnable onTarget){
        this.target = target;
        this.onTarget = onTarget;
    }

    @Override
    public String toString() {
        return "Count is " + count;
    }

    public static void main(String[] args) {
        Counter c0 = new Counter();
        c0.tick();
        Counter c = Counter.plus(new Counter(), c0);
        c.onTarget(50, () -> System.out.println("Reached 50!"));
        for (int i = 0; i < 100; i++){
            c.tick();
        }
        System.out.println(c);
    }
}
