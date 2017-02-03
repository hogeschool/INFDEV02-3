package chapter1.samples;

public class Sprint {
    UserStory[] userStories;

    public static void main(String[] args) {
        Sprint sprint = new Sprint();
        sprint.userStories = new UserStory[3];

        UserStory us = new UserStory();
        us.setDescription("Als gebruiker wil ik in kunnen loggen.");
        us.setHours(10);
        sprint.userStories[0] = us;

        us = new UserStory();
        us.setDescription("Als gebruiker wil ik uit kunnen loggen");
        us.setHours(5);
        sprint.userStories[1] = us;

        us = new UserStory();
        us.setDescription("Als gebruiker wil ik ");
        us.setHours(1);
        sprint.userStories[2] = us;

    }

    public void totalHours(){
        int sum = 0;

        for(UserStory userStory: userStories){
            if (userStory != null) {
                sum += userStory.getHours();
            }
        }

        System.out.println(sum);
    }
}
