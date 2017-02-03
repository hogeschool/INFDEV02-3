package chapter1.scrum_advanced;

import java.time.Duration;
import java.time.LocalDate;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;

public class Sprint {
    ArrayList<UserStory> userStories;
    LocalDate startDate;
    Duration duration;

    public Sprint(LocalDate startDate){
        userStories = new ArrayList<>();
        this.startDate = startDate;
        this.duration = Duration.of(2, ChronoUnit.WEEKS);
    }

    public void addUserStory(UserStory userStory){
        userStories.add(userStory);
    }

    public int totalHours(){
        return userStories.stream()
                .mapToInt(UserStory::getHours)
                .sum();
    }

    public int hoursUncompleted(){
        return userStories.stream()
                .filter(userStory -> !userStory.getProgress().equals(Progress.DONE))
                .mapToInt(UserStory::getHours)
                .sum();
    }

    public int hoursCompleted() {
        return totalHours() - hoursUncompleted();
    }
}
