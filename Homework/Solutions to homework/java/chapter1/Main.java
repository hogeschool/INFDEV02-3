package chapter1;

import chapter1.scrum.UserStory;

public class Main {
    public static void main(String[] args) {
        UserStory us1 = new UserStory("Als user wil ik inloggen", 10);
        UserStory us2 = new UserStory("Als user wil ik inloggen", 100);

        System.out.println(us1);
        System.out.println(us2);
        System.out.println(us1 == us2);

        System.out.println(us1.equals(us2));

    }
}
