package chapter1.scrum;

public class Main {

    public static void main(String[] args) {
        Sprint sprint = new Sprint();

        UserStory us = new UserStory("Als gebruiker wil ik in kunnen loggen.", 10, "todo");
        sprint.addUserStory(us);

        us = new UserStory("Als gebruiker wil ik uit kunnen loggen.", 10000, "Done");
        sprint.addUserStory(us);

        System.out.println("Total hours in this sprint: " + sprint.totalHours());
        System.out.println("Completed hours: " + sprint.hoursCompleted());
        System.out.println("Work to be done: " + sprint.hoursUncompleted());

    }
}
