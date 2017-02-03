package drills.drill2;

public class Main {

    public static void main(String[] args) {
        UserStory_encapsulated u1 = new UserStory_encapsulated();
        u1.setDescription("Als user wil ik een sprint backlog, zodat ik userstories kan opslaan");
        u1.setHours(50);

        UserStory_encapsulated u2 = new UserStory_encapsulated();
        u2.setDescription("Als programmeur wil ik taken, zodat ik userstories kan splitsen");
        u2.setHours(50);

        UserStory_encapsulated[] stories = new UserStory_encapsulated[2];
        stories[0] = u1;
        stories[1] = u2;

        int totalHours = 0;
        for (int i = 0; i < stories.length; i++) {
            totalHours += stories[i].getHours();
        }

        System.out.println(totalHours);
    }
}
