package drills.drill2;


/**
 * The UserStory-class is given: it only has a description and hours.
 *  In this variant of the class, the fields (description and hours) must be made private.
 *  To expose these fields; getters and setters must be made.
 *  Think of useful validation rules for the setters.
 *
 *  After that:
 * - Instantiate 2 UserStory-objects.
 * - Put them in an array (HFJ P.59)
 * - print the total amount of hours using a loop on that array
 */
public class UserStory_encapsulated {
    private String description;
    private int hours;

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public int getHours() {
        return hours;
    }

    public void setHours(int hours) {
        if(hours > 0) {
            this.hours = hours;
        }else{
            System.err.println("Hours should be bigger than 0");
            this.hours = 0;
        }
    }
}
