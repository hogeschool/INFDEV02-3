package chapter1.scrum_advanced;

import java.time.LocalDate;

public class Main {

    public static void main(String[] args) {
        Sprint sprint = new Sprint( LocalDate.of(2016, 2, 19));

        UserStory us = new UserStory("Als gebruiker wil ik in kunnen loggen.", 10, Progress.TODO);
        sprint.addUserStory(us);

        us = new UserStory("Als gebruiker wil ik uit kunnen loggen.", 10000, Progress.DONE);
        sprint.addUserStory(us);

        System.out.println("Total hours in this sprint: " + sprint.totalHours());
        System.out.println("Completed hours: " + sprint.hoursCompleted());
        System.out.println("Work to be done: " + sprint.hoursUncompleted());

    }
}
