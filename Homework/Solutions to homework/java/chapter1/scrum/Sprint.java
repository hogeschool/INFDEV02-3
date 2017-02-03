package chapter1.scrum;

import java.util.ArrayList;

public class Sprint {
    ArrayList<UserStory> userStories;

    public Sprint(){
        userStories = new ArrayList<>();
    }

    public void addUserStory(UserStory userStory){
        userStories.add(userStory);
    }

    public int totalHours(){
        int sum = 0;
        for(UserStory userStory: userStories){
            sum += userStory.getHours();
        }
        return sum;
    }

    public int hoursUncompleted(){
        int sum = 0;
        for(UserStory userStory: userStories){
            if ( !userStory.getStatus().equals("Done")) {
                sum += userStory.getHours();
            }
        }
        return sum;
    }

    public int hoursCompleted() {
        return totalHours() - hoursUncompleted();
    }


}
