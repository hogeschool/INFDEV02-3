package chapter2.samples;

public class Main {
    public static void main(String[] args) {
        Person p1 = new Player();
        p1.naam = "Ferdi";

        Coach c1 = new Coach();
        c1.support();
    }
}

class Person{
    String naam;
}

class Player extends Person implements Supporter, Run{
    int rugnummer;

    void play(){
        System.out.println("playing");
    }

    public void support(){
        System.out.println(naam + ": geeft schouderklopje");
    }

    public void run(){
        System.out.println("running");
    }
}

class Coach extends Person implements Supporter{
    int salaris;

    public void support(){
        System.out.println(naam + ": ren harder!");
    }
}

interface Supporter{
    void support();
}

interface Run{
    void run();
}
