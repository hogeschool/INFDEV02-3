package chapter2.animals;

import java.util.Arrays;
import java.util.List;

//This is a slightly more advanced demonstration of the animal orchestra
public class Main2 {
    public static void main(String[] args) {
        //Because cats, dogs and cows are all of type Animal, they fit in the same list
        //Note that 'List' is an interface itself
        List<Animal> animals = Arrays.asList(
                new Cat(),
                new Cat(),
                new Dog(),
                new Dog(),
                new Cow(),
                new Cow(),
                new Cow()
        );

        //all make some noise
        for (Animal animal : animals) {
            animal.saySomething();
        }

        //empty line
        System.out.println();


        //a shorter noise maker.
        animals.forEach(Animal::saySomething);
    }
}
