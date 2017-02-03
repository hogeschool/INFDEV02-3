package chapter2.sample2;

public class Main {
    Player player;

    public static void main(String[] args) {
        Support player = new Player();
        player.support();
        //System.out.println(player.naam);

    }
}

interface Support{
    void support();
}

interface Run{
    void run();

    void crawl();
}
class Person{
    public String naam;

}

class Player extends Person implements Support, Run{
    int rugnummer;

    public void support(){
        System.out.println("goed zo?");
    }

    public void run(){
        System.out.println("ren");
    }

    public void crawl(){

    }

}

class Coach extends Person implements Support{
    public void support(){
        System.out.println("rennen!");
    }
}
class Supporter extends Person implements Support{

    @Override
    public void support() {

    }
}
class Kid extends Person implements Run{
    @Override
    public void run() {

    }

    @Override
    public void crawl() {

    }
}