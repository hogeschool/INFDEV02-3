package drills.drill2;


/**
 * The UserStory-class is given: it only has a description and hours.
 * - Instantiate 2 UserStory-objects.
 * - Put them in an array (HFJ P.59)
 * - print the total amount of hours using a loop on that array
 */
public class UserStory {
    String description;
    int hours;


    public static void main(String[] args) {
        UserStory u1 = new UserStory();
        u1.description = "Als user wil ik een sprint backlog, zodat ik userstories kan opslaan";
        u1.hours = 50;
        
        UserStory u2 = new UserStory();
        u2.description = "Als programmeur wil ik taken, zodat ik userstories kan splitsen";
        u2.hours = 50;

        UserStory[] stories = new UserStory[2];
        stories[0] = u1;
        stories[1] = u2;

        int totalHours = 0;
        for (int i = 0; i < stories.length; i++) {
            totalHours += stories[i].hours;
        }

        System.out.println(totalHours);
    }
}
